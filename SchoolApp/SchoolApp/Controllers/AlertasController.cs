 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;

namespace SchoolApp.Controllers
{
    public class AlertasController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IAlertaRepository _alertaRepository;

        public AlertasController(DataContext context, IUserHelper userHelper, IAlertaRepository alertaRepository)
        {
            _context = context;
            _userHelper = userHelper;
            _alertaRepository = alertaRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
           var reports= _alertaRepository.GetAll().Include(a => a.User).ToList();
            
            return View(reports);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DetailsAlertaAdmin(int? id)
        {
            if (id == null)
            {
                  
                return View("Error");
            }

            var alerta = await _alertaRepository.GetAll()
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alerta == null)
            {
                return View("Error");
            }

            return View(alerta);
        }
        [HttpPost]
        public async Task<IActionResult> DetailsAlertaAdmin(int id,string estado)
        {
          

            var alerta = await _alertaRepository.GetAll()
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            var user =await _userHelper.GetUserByIdAsync(alerta.UserId);

            alerta.User = user;
            alerta.Estado = estado;
            if (alerta == null)
            {
                return View("Error");
            }
            try
            {
                await _alertaRepository.UpdateAsync(alerta);
                ViewBag.message = "O estado deste Alerta foi atualizado com sucesso";
                return RedirectToAction("AdminIndex", "Alertas");
            }
            catch
            {
                ViewBag.message = "Ocurreu um problema ao atualizar o estado deste Alerta";
            }
            return View(alerta);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> IndexAlertasFunc(string estado)
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var alertasdofuncionario = _alertaRepository.GetAll().Include(p => p.User).Where(p => p.UserId == user.Id);
            return View(alertasdofuncionario);
        }
      
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Create()
        {
           var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var alerta = new Alerta
            {
                User = user,
                UserId = user.Id,
                Estado = "Enviado"
            };
            return View(alerta);

        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alerta Alerta)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var alerta = new Alerta
                {
                    User = user,
                    UserId = user.Id,
                    Assunto = Alerta.Assunto,
                    Problema = Alerta.Problema,
                    Data = DateTime.Today,
                    Estado = "Enviado"
                };
                try
                {
                    await _alertaRepository.CreateAsync(alerta);
                    ViewBag.message = "O seu Alerta foi criado com Sucesso, o Admin poderá vê-lo a qualquer momento";
                    return RedirectToAction("IndexAlertasFunc", "Alertas");
                }catch{

                    ViewBag.TituloErro = "Erro ao Criar Alerta";
                    ViewBag.MensagemErro = "Ocurreu um erro ao criar o seu alerta";
                    return View("Error");
                }
   
            }
           
            return View(Alerta);
        }

       
     
       

 
    }
}
