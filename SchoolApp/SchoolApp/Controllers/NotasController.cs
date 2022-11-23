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
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class NotasController : Controller
    {

        private readonly INotaRepository _notaRepository;
        private readonly IAlunosRepository _alunosRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly ICursoDisciplinarRepository _turmaDisciplinarRepository;
        private readonly IUserHelper _userHelper;
        private readonly ICursoRepository _cursoRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;

        public NotasController(INotaRepository notaRepository, IAlunosRepository alunosRepository, IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository, ICursoDisciplinarRepository turmaDisciplinarRepository, IUserHelper userHelper, ICursoRepository cursoRepository)
        {

            _notaRepository = notaRepository;
            _alunosRepository = alunosRepository;
            _turmasRepository = turmasRepository;
            _turmaDisciplinarRepository = turmaDisciplinarRepository;
            _userHelper = userHelper;
            _cursoRepository = cursoRepository;
            _disciplinasRepository = disciplinasRepository;
        }

        // GET: Notas
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> Index()
        {
           
            var alunos = await _notaRepository.GetNotasAlunoAsync();
            if (alunos.Any())
            {
                foreach (var aluno in alunos)
                {
                    aluno.Cursos = await _notaRepository.GetComboTurmasporAlunoAsync(aluno.alunoId);
                }
            }

            return View(alunos);
        }
        
        [HttpPost]
        public IActionResult Index(IEnumerable<NotaAlunoViewModel> model, int AlunoId)
        {
            
            if (ModelState.IsValid)
            {
                int cursoid = 0;
                foreach(var item in model)
                {
                    if(item.alunoId == AlunoId)
                    {
                        cursoid = item.CursoId;
                    }
                }
                return RedirectToAction("IndexNotasAluno", "Notas", new { AlunoId, cursoid });
            } 
            
            return View(model);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> IndexNotasAluno(int AlunoId, int cursoid)
        {
      
            if (AlunoId == 0)
            {
                return RedirectToAction("Index", "Notas");
            }
            if(cursoid == 0)
            {
                var model1 = new TodasNotasDoAlunoViewModel();
                ViewBag.errormessage = "Este aluno ainda não tem notas";
                return View(model1);
            }
            var aluno = await _alunosRepository.GetByIdAsync(AlunoId);
            var curso = await _cursoRepository.GetByIdAsync(cursoid);
            
  

            var model = new TodasNotasDoAlunoViewModel()
            {
                Nome = aluno.PrimeiroNome + " " + aluno.UltimoNome,
                foto = aluno.ImageUrl,
                Curso = curso.Nome,
                
                Notas = await _notaRepository.GetNotasAlunoDaTurma(aluno.Id, curso.Id)
                
                
            };
            


            return View(model);
        }
        [Authorize(Roles = "Funcionario")]
        public IActionResult CreateTurmaNota(int turmaid)
        {
            var model = new NotaTurmaViewModel
            {
                Turmaid = turmaid,
                Turmas = _turmasRepository.GetComboTurmas()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateTurmaNota(NotaTurmaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateDisciplinaNota", "Notas", new { turmaid = model.Turmaid });
            }
            model.Turmas = _turmasRepository.GetComboTurmas();
            return View(model);
        }
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> CreateDisciplinaNota(int turmaid, int disciplinaid)
        {
            if(turmaid== 0)
            {
                return View("Error");
            }

            var turma = await _turmasRepository.GetByIdAsync(turmaid);
            var curso = await _cursoRepository.GetByIdAsync(turma.CursoId);
            var model = new NotaDisciplinaViewModel
            {
              CursoId = curso.Id,
              CursonNome = curso.Nome,
              Turmaid= turma.Id,
              Nometurma = turma.Nome,
              Disciplinaid = disciplinaid,
              Disciplinas =await  _disciplinasRepository.GetComboDisciplinasporCursoAsync(curso.Id)
            };
            return View(model);
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateDisciplinaNota(NotaDisciplinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateAlunoNota", "Notas", model);
            }
            model.Disciplinas = await _disciplinasRepository.GetComboDisciplinasporCursoAsync(model.CursoId);
            return View(model);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> CreateAlunoNota(NotaDisciplinaViewModel model2)
        {
            if (ModelState.IsValid)
            {
                var disciplina = await _disciplinasRepository.GetByIdAsync(model2.Disciplinaid);

                var model = new NotaAlunoTurmaDisciplinaViewModel
                {
                    disciplinaId = model2.Disciplinaid,
                    disciplinaNome = disciplina.Nome,
                    CursoNome = model2.CursonNome,
                    TurmaId = model2.Turmaid,
                    turmaNome = model2.Nometurma,
                    Duracao = disciplina.Duracao,
                    Alunos = (await _notaRepository.GetNotasAlunoDaTurmaDisciplina(model2.Turmaid, model2.Disciplinaid)).ToList()
                };
                return View(model);
            }

            return RedirectToAction("CreateTurmaNota", "Notas");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlunoNota(NotaAlunoTurmaDisciplinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var turma = await _turmasRepository.GetByIdAsync(model.TurmaId);
                var disciplina = await _disciplinasRepository.GetByIdAsync(model.disciplinaId);
                var alunos = await _alunosRepository.GetByIdAsync(model.Alunos.FirstOrDefault().alunoid);

                try
                {
                    foreach(var aluno  in model.Alunos)
                    {

                     if(aluno.novanota != null)
                        {
                            if (aluno.nota == null)
                            {
                                await _notaRepository.CreateAsync(new Nota
                                {
                                    idaluno =aluno.alunoid,
                                    idturma =model.TurmaId,
                                    iddisciplina = model.disciplinaId,
                                    Data = model.Data,
                                    NotaAluno = aluno.novanota.Value,
                               
                                   

                                    
                                });
                            }

                            else if (aluno.nota != null)
                            {
                                var nota = await _notaRepository.GetNotaByDados(aluno.alunoid, turma.Id, disciplina.Id);

                                nota.NotaAluno = aluno.novanota.Value;
                                nota.Data = model.Data;

                                await _notaRepository.UpdateAsync(nota);
                            }
                        }
                        
                    }
                }catch(Exception ex)
                {
                    ViewBag.message= ex.Message;
                }

                var modelparaview = new NotaDisciplinaViewModel
                {
                    Disciplinaid = model.disciplinaId,
                    Nometurma = model.turmaNome,
                    Turmaid = model.TurmaId,
                    CursonNome =model.CursoNome,
                    Alunos = model.Alunos
                    
                   
                    
                };



                return RedirectToAction("CreateAlunoNota", "Notas", modelparaview);
            }
            return View(model);
        }
   
    }
}
