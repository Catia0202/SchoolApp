using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IImageHelper _imagehelper;
        private readonly UserManager<User> _userManager;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly DataContext _context;

        public AccountController(IImageHelper imageHelper,UserManager<User> userManager,IUserHelper userHelper, IConfiguration configuration, IMailHelper mailHelper, DataContext context )
        {
            _imagehelper = imageHelper;
            _userManager = userManager;
            _userHelper = userHelper;
            _configuration = configuration;
           _mailHelper = mailHelper;
           _context = context;
        }


        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);

                if (user != null)
                {
                    if (!user.passwordchanged)
                    {
                        ViewBag.errorMessage = "Foi enviado um email para mudar a sua palavra-passe, faça isso e tente novamente";
                        return View();
                    }
                }
                var result = await _userHelper.LoginAsync(model);
           
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                   if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("HomeAdmin", "Home");
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Dados Incorretos");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                var path = string.Empty;
                if (model.ProfilePictureFile != null && model.ProfilePictureFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ProfilePictureFile, "alunos");
                }
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.Username,
                        Email = model.Username,
                        Password = model.Password,
                        ProfilePicture = path
                        

                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    await _userHelper.AddUserToRoleAsync(user, model.Role);


                    string tokenLink = Url.Action("ChangeInitPassword", "Account", new
                    {
                        userid = user.Id,
                       
                    },protocol: HttpContext.Request.Scheme);

                  Response response=  _mailHelper.SendEmail(model.Username, "Passoword Change",
                        $"<h1>Password Confirmation </h1" +
                        $"To allow user," +
                        $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email </a>");

                    if (result.Succeeded)
                    {
                        return RedirectToAction("HomeAdmin", "Home");
                    }

                }

            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult IndexAllUsers()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
           
              if(user == null)
            {
                ViewBag.TituloErro = "Erro ao atualizar User";
                ViewBag.MensagemErro = "Ocurreu um erro ao atualizar o user";
                return View("Error");
            }
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.profilepicturepath = user.ProfilePicture;
                
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var path = string.Empty;
                if (model.ProfilePictureFile != null && model.ProfilePictureFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ProfilePictureFile, "users");
                }
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.ProfilePicture = path;
                    model.profilepicturepath = path;
                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User uptaded!";
                    }
                    else
                    {
                        ViewBag.TituloErro = "Erro ao atualizar User";
                        ViewBag.MensagemErro = "Ocurreu um erro ao atualizar o user";
                        return View("Error");
                    }
                }
            }
            return View(model);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(this.User.Identity.Name != null)
                {
                    var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    if (user != null)
                    {
                        var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                        if (result.Succeeded)
                        {
                            return this.RedirectToAction("ChangeUser");
                        }
                        else
                        {
                            ViewBag.TituloErro = "Erro ao mudar Password";
                            ViewBag.MensagemErro = "Ocurreu um erro ao mudar Password";
                            return View("Error");
                        }
                    }
                else
                {
                    ViewBag.TituloErro = "Utilizador";
                    ViewBag.MensagemErro = "Utilizador não encontrado";
                    return View("Error");
                }
                }
                else
                {
                 
                    return View("Error");
                }
            }


            return this.View(model);
        }
        public IActionResult ChangeInitPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInitPassword(ChangeInitPasswordViewModel model,string userid)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(userid);
                user.Password = model.NewPassword;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,model.NewPassword); //changepasswordasync not working????
                user.passwordchanged = true;
                
                if (user != null)
                {
                    var result = await _userHelper.UpdateUserAsync(user);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeInitPassword");
                    }
                    else
                    {
                        ViewBag.TituloErro = "Erro ao mudar Password";
                        ViewBag.MensagemErro = "Ocurreu um erro ao mudar Password";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.TituloErro = "Utilizador";
                    ViewBag.MensagemErro = "Utilizador não encontrado";
                    return View("Error");
                }
            }


            return this.View(model);
        }
        public IActionResult RecoverPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);

                if (user == null)
                {
                    ViewBag.TituloErro = "Utilizador";
                    ViewBag.MensagemErro = "Utilizador não encontrado";
                    return View("Error");
                   
                }

                var myToken = await  _userManager.GeneratePasswordResetTokenAsync(user);

                var link = Url.Action
                    (
                        "ChangeInitPassword",
                        "Account", 
                        new {userid= user.Id ,token = myToken },
                        protocol: HttpContext.Request.Scheme
                    );

                Response response = _mailHelper.SendEmail
                    (
                        model.Email,
                        "Password Reset",
                      
                        $"Para resetar a sua palabra passe clique  <a href = \"{link}\">aqui</a" 
                    );

                if (response.IsSuccess)
                {
                    ViewBag.Message = "<span class=\"text-success\">Foi lhe enviado um email. Aceda ao email e siga as instruções</span>";
                }

                return View();
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                if(user!= null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return this.Created(string.Empty,results);

                            
                               
                    }
                }
            }
            return BadRequest();
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                ViewBag.TituloErro = "Utilizador";
                ViewBag.MensagemErro = "Utilizador não encontrado";
                return View("Error");
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                ViewBag.TituloErro = "Utilizador";
                ViewBag.MensagemErro = "Utilizador não encontrado";
                return View("Error");
            }

          
            var result = await _userHelper.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return View("Error");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
             
                return View("Error");
            }

            var user = await _userHelper.GetUserByIdAsync(id);
            
            if (user == null)
            {
              
                return View("Error");
            }
            return View(user);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
       
            var user = await _userHelper.GetUserByIdAsync(id);
       
            if(user == null)
            {
             
                return View("Error");
            }

           
                await _userManager.DeleteAsync(user);

            return RedirectToAction("HomeAdmin", "Home");



        }
    }

}
