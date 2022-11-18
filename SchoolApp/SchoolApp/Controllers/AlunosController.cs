using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class AlunosController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailHelper _mailHelper;
        private readonly IAlunosRepository _alunosRepository;
        private readonly IImageHelper _imagehelper;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ITurmasRepository _turmasRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;

        public AlunosController(UserManager<User> userManager,IMailHelper mailHelper,IAlunosRepository alunosRepository, IImageHelper imagehelper, IUserHelper userHelper,  IConverterHelper converterHelper,ITurmasRepository turmasRepository, IDisciplinasRepository disciplinasRepository, IConfiguracaoRepository configuracaoRepository)
        {
            _userManager = userManager;
            _mailHelper = mailHelper;
            _alunosRepository = alunosRepository;
            _imagehelper = imagehelper;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _turmasRepository = turmasRepository;
            _disciplinasRepository = disciplinasRepository;
            _configuracaoRepository = configuracaoRepository;
        }

      
        public IActionResult Index() 
        {
            var model = Enumerable.Empty<AlunoViewModel>();

                var alunos = _alunosRepository.GetAll().Include(p => p.turma);
               
                if (alunos.Any())
                {
                
              
                    model = (_converterHelper.AlunosToAlunoViewModels(alunos)).OrderBy(x => x.PrimeiroNome);
                }
                   
             
                else
                {
                    ViewBag.message = "Não foram encontrados alunos";
                }
            return View(model);
        
        }
        //[HttpGet]
        //public IActionResult Index(string pesquisa)
        //{
        //    var model = Enumerable.Empty<AlunoViewModel>();

        //    var alunos = _alunosRepository.GetAll().Include(p => p.turma);
        //    if (pesquisa != null)
        //         {
        //        model = (_converterHelper.AlunosToAlunoViewModels(alunos)).Where(p => p.Nome == pesquisa);
        //        }
        //    return View(model);
        //}
            // GET: Alunos/Details/5
            public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetAlunoByIdWithTurmaAsync(id);
  
            if (aluno == null)
            {
                return NotFound();
            }

           




            return View(aluno);
        }

        // GET: Alunos/Create
        [Authorize(Roles ="Funcionario")] //para varios role's fica  [Authorize(Roles ="Admin,Customer,SuperUser")]
        public IActionResult Create()
        {
            var model = new AlunoViewModel
            {

                Turmas = _turmasRepository.GetComboTurmas()
            };
            return View(model);
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                var adminconfig = _configuracaoRepository.GetAll().FirstOrDefaultAsync();
                
                
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imagehelper.UploadImageAsync(model.ImageFile, "alunos");
                }
                int alunosjanaturma = _alunosRepository.GetAll().Where(p => p.turmaid == model.turmaid).Count();
                //TODO:Modificar para o user que tiver logado
         
                if(adminconfig.Result.MaximoAlunosNaTurma > alunosjanaturma)
                {
                    var aluno = _converterHelper.ToAluno(model, path, true);

                    await _alunosRepository.CreateAsync(aluno);
                    var user = await _userHelper.GetUserByEmailAsync(aluno.Email);
                    if (user == null)
                    {
                        user = new User
                        {

                            FirstName = aluno.PrimeiroNome,
                            LastName = aluno.UltimoNome + "Last",
                            Email = aluno.Email,
                            UserName = aluno.Email,
                            Password = aluno.PrimeiroNome + "123456"

                        };

                        var result = await _userHelper.AddUserAsync(user, user.Password);
                        aluno.User = user;
                        if (result != IdentityResult.Success)
                        {
                            throw new InvalidOperationException("Could not create the user in seeder");
                        }
                        await _userHelper.AddUserToRoleAsync(user, "Aluno");

                        string tokenLink = Url.Action("ChangeInitPassword", "Account", new
                        {
                            userid = user.Id

                        }, protocol: HttpContext.Request.Scheme);

                        Response response = _mailHelper.SendEmail(aluno.Email, "Passoword Change",
                              $"<h1>Password Confirmation </h1" +
                              $"To allow user," +
                              $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email </a>");








                    }


                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewBag.errormessage = "Não foi possível inserir o aluno neste curso pois exedeu o número máximo de alunos por Curso";
                }
            }


            model.Turmas = _turmasRepository.GetComboTurmas();
            return View(model);
        }


        private Aluno toAluno(AlunoViewModel model, string path)
        {
            return new Aluno
            {
                Id = model.Id,
                ImageUrl= path ,
                PrimeiroNome = model.PrimeiroNome,
                Data_Nascimento = model.Data_Nascimento,
                Email = model.Email,
                Genero = model.Genero,
                Morada = model.Morada,
                Telemovel = model.Telemovel,
                User = model.User,
                turmaid = model.turmaid,
                turma=model.turma
                

            };
        }
        // GET: Alunos/Edit/5
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetAlunoByIdWithTurmaAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            var user = await _userHelper.GetUserByEmailAsync(aluno.Email);
            var model = _converterHelper.ToAlunoViewModel(aluno,user);
            model.Turmas = _turmasRepository.GetComboTurmas();
            model.User = await _userHelper.GetUserByEmailAsync(aluno.Email);
            model.Antigoemail = aluno.Email;
            return View(model);
        }
        //private AlunoViewModel ToAlunoViewModel(Aluno aluno)
        //{
        //    return new AlunoViewModel
        //    {
        //        Id = aluno.Id,
        //        ImageUrl = aluno.ImageUrl,
        //        Nome = aluno.Nome,
        //        Data_Nascimento = aluno.Data_Nascimento,
        //        Email = aluno.Email,
        //        Genero = aluno.Genero,
        //        Morada = aluno.Morada,
        //        Telemovel = aluno.Telemovel,
        //        User = aluno.User,
        //        turmaid =aluno.turmaid
        //    };
        //}
        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AlunoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var adminconfig = _configuracaoRepository.GetAll().FirstOrDefaultAsync();
                    int alunosjanaturma = _alunosRepository.GetAll().Where(p => p.turmaid == model.turmaid).Count();
                    var path = model.ImageUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imagehelper.UploadImageAsync(model.ImageFile, "alunos");
                    }

                    if (adminconfig.Result.MaximoAlunosNaTurma > alunosjanaturma)
                    { 

                        var aluno = _converterHelper.ToAluno(model, path, false);


                        var user = await _userHelper.GetUserByEmailAsync(model.Antigoemail);
                        if (user != null)
                        {
                            user.Email = aluno.Email;
                            user.FirstName = aluno.PrimeiroNome;
                            user.LastName = aluno.UltimoNome;
                            user.UserName = aluno.Email;

                        }



                        await _alunosRepository.UpdateAsync(aluno);
                        await _userHelper.UpdateUserAsync(user);
                        model.Turmas = _turmasRepository.GetComboTurmas();


                    }
                    else
                    {
                        ViewBag.errormessage = "Não foi possível inserir o aluno neste curso pois exedeu o número máximo de alunos por Curso";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _alunosRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
         
            model.Turmas = _turmasRepository.GetComboTurmas();
            return View(model);
        }

        // GET: Alunos/Delete/5
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _alunosRepository.GetByIdAsync(id.Value);
            
            if (aluno == null)
            {
                return NotFound();
                          }
                return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _alunosRepository.GetByIdAsync(id);
            var user = await _userHelper.GetUserByEmailAsync(aluno.Email);
            var turma = await _turmasRepository.GetByIdAsync(id);


            try
            {
                await _alunosRepository.DeleteAsync(aluno);
                await _userManager.DeleteAsync(user);
                ViewBag.errormessage = "Aluno e User associado deletados com sucesso!";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {

                    ViewBag.errormessage = "Esta Aluno não pode ser deletada pois está a ser utilizada";
                }

                return View();
            }


            return RedirectToAction(nameof(Index));

     
    

        }

        public IActionResult IndexAlunosDaTurma(int id, int id2)
        {
            var model = Enumerable.Empty<AlunoViewModel>();
            if (id != 0)
            {
                ViewBag.data = id2;
                var alunos = _alunosRepository.GetAll().Include(p => p.turma).Where(p => p.turmaid == id);
                if (alunos.Any())
                {
                    model = (_converterHelper.AlunosToAlunoViewModels(alunos)).OrderBy(x => x.UltimoNome);
                }
                else
                {
                    ViewBag.message = "Não foram encontrados alunos";
                }


            }
            return View(model);
        }
        [Authorize(Roles ="Aluno")]
        public async Task<IActionResult> AlunoAvaliacoes()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            var aluno = _alunosRepository.GetAll().Where(p => p.User.Id == user.Id).FirstOrDefault();

            var turma = _turmasRepository.GetByIdAsync(aluno.turmaid);

            var model = new AlunoAvaliacoesViewModel
            {
                NomeCurso = turma.Result.Nome,
                AvaliacaoDisciplinas = await _alunosRepository.GetAvaliacaoAlunoEmDisciplinaAsync(aluno.Id, turma.Result.Id)
            };
            return View(model);
        }

        ////private bool AlunoExists(int id)
        //{
        //    return _context.Aluno.Any(e => e.Id == id);
        //}
    }
}
