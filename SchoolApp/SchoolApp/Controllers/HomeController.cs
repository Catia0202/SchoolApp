using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IConfiguracaoRepository _configuracaoRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly ICursoDisciplinarRepository _turmaDisciplinarRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly IAlunosRepository _alunosRepository;
        private readonly ICursoRepository _cursoRepository;

        public HomeController(ILogger<HomeController> logger, DataContext context, IConfiguracaoRepository configuracaoRepository,ITurmasRepository turmasRepository, ICursoDisciplinarRepository turmaDisciplinarRepository, IDisciplinasRepository disciplinasRepository, IAlunosRepository alunosRepository, ICursoRepository cursoRepository)        {
            _logger = logger;
            _context = context;
            _configuracaoRepository = configuracaoRepository;
            _turmasRepository = turmasRepository;
            _turmaDisciplinarRepository = turmaDisciplinarRepository;
            _disciplinasRepository = disciplinasRepository;
            _alunosRepository = alunosRepository;
            _cursoRepository = cursoRepository;
        }

        public  IActionResult Index()
        {
            var model = new IndexCursosViewModel
            {
               Cursos  =  _cursoRepository.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> IndexTurmaDisciplinas(int idcurso)
        {
            var curso = _context.Cursos.Where(p=> p.Id== idcurso).FirstOrDefault();
            var model = new IndexTurmasDisciplinasViewModel
            {
                DisciplinasDaTurma = await _disciplinasRepository.GetIndexTurmasDisciplinasAsync(idcurso),
                Curso= curso,
                Cursos = await _cursoRepository.GetIndexCursoAsync()
            };



            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize(Roles="Admin")]
        public IActionResult HomeAdmin()
        {
            HomeAdminViewModel model = new HomeAdminViewModel
            {
                TotalAlunos = _alunosRepository.GetAll().Count(),
                TotalDisciplinas = _disciplinasRepository.GetAll().Count(),
                TotalTurmas=_turmasRepository.GetAll().Count(),
                TotalCursos = _cursoRepository.GetAll().Count(),
                Users = (from userroles in _context.UserRoles
                        join user in _context.Users
                        on userroles.UserId equals user.Id
                        join roles in _context.Roles 
                        on userroles.RoleId equals roles.Id
                        select new
                        {
                            UserId = user.Id,
                            Username = user.UserName,
                            Email = user.Email,
                            Role = roles.Name
                        }).Where(p => p.Role == "Funcionario" || p.Role == "Admin").ToList().Select(p => new UsersComRoles
                        {
                            UserId = p.UserId,
                           Username = p.Username,
                           Email = p.Email,
                           Role = p.Role
                        })
            

        };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
  
        public  ActionResult ConfigAdmin()  
        {
            var config =_configuracaoRepository.GetAll().FirstOrDefault();

            return View(config);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("ConfigAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfigAdmin(Configuracao configuracao)
        {
            if (configuracao.Id == 0)
            {
                return RedirectToAction("HomeAdmin");
            }

            if(configuracao == null){
                ViewBag.errormessage = "Nenhuma Configuração Encontrada";
                return View("Error");
            }
              

           await  _configuracaoRepository.UpdateAsync(configuracao);
            return View(configuracao);
        }
    }
}
