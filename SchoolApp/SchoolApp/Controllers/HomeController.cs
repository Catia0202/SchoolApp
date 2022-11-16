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
        public IActionResult HomeAdmin(HomeAdminViewModel model)
        {
            new HomeAdminViewModel
            {
                TotalAlunos = _alunosRepository.GetAll().Count(),
                TotalDisciplinas = _disciplinasRepository.GetAll().Count(),
                TotalTurmas=_turmasRepository.GetAll().Count(),
                
            };
            return View();
        }
        [Authorize(Roles = "Admin")]
  
        public  ActionResult ConfigAdmin()  
        {
            var config =_configuracaoRepository.GetAll().FirstOrDefault();

            return View(config);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditConfigAdmin(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("HomeAdmin");
            }

            var config = await _configuracaoRepository.GetByIdAsync(id);
            if(config == null){
                ViewBag.errormessage = "Nenhuma Configuração Encontrada";
                return View("Error");
            }
   

           await  _configuracaoRepository.UpdateAsync(config);
            return RedirectToAction("ConfigAdmin");
        }
    }
}
