using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class FaltasController : Controller
    {
        private readonly DataContext _context;
        private readonly IFaltaRepository _faltaRepository;
        private readonly IAlunosRepository _alunosRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;

        public FaltasController(DataContext context, IFaltaRepository faltaRepository, IAlunosRepository alunosRepository, ITurmasRepository turmasRepository, IDisciplinasRepository disciplinasRepository)
        {
            _context = context;
            _faltaRepository = faltaRepository;
            _alunosRepository = alunosRepository;
            _turmasRepository = turmasRepository;
            _disciplinasRepository = disciplinasRepository;
        }

        // GET: Faltas
        public async Task<IActionResult> Index()
        {
            var alunos = await _faltaRepository.GetFaltasAlunoAsync();
            if (alunos.Any())
            {
                foreach (var aluno in alunos)
                {
                    aluno.Turmas = _faltaRepository.GetComboTurmasporAlunoAsync(aluno.alunoId);
                }
            }

            return View(alunos);
        }

        [HttpPost]
        public IActionResult Index(int AlunoId)
        {
            var model = new FaltaAlunoViewModel();
            if (ModelState.IsValid)
            {
                var aluno = _alunosRepository.GetAll().Where(p => p.Id == AlunoId);

                var turmaid = aluno.FirstOrDefault().turmaid;


                model = new FaltaAlunoViewModel
                {
                    alunoId = aluno.FirstOrDefault().Id,
                    Foto = aluno.FirstOrDefault().ImageUrl,
                    Nome = aluno.FirstOrDefault().Nome,
                    TurmaId = turmaid
                };
                return RedirectToAction("IndexFaltasAluno", "Faltas", new { AlunoId, turmaid });
            }

            return View(model);
        }
        public async Task<IActionResult> IndexFaltasAluno(int AlunoId, int turmaid)
        {
            if (AlunoId == 0)
            {
                return RedirectToAction("Index", "Faltas");
            }

            var aluno = await _alunosRepository.GetByIdAsync(AlunoId);
            var turma = await _turmasRepository.GetByIdAsync(turmaid);
       


            var model = new TodasFaltasdoAlunoViewModel
            {
                Nome = aluno.Nome,
                foto = aluno.ImageUrl,
                Turma = turma.Nome,
                Faltas = await _faltaRepository.GetFaltasAlunoDaTurma(aluno.Id, turmaid)
            };
            ///resolver depoisss


            return View(model);
        }

        public IActionResult CreateTurmaFaltas (int idturma)
        {
            var model = new FaltaTurmaViewModel
            {
                turmaid = idturma,
                Turmas = _turmasRepository.GetComboTurmas()
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateTurmaFaltas(FaltaTurmaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateDisciplinaFaltas", "Faltas", new { turmaid = model.turmaid });
            }
            model.Turmas = _turmasRepository.GetComboTurmas();
            return View(model); 
        }

        public async Task<IActionResult> CreateDisciplinaFaltas(int turmaid, int disciplinaid)
        {
            if (turmaid == 0)
            {
                return RedirectToAction("CreateTurmaFaltas", "Faltas");
            }

            var turma = await _turmasRepository.GetByIdAsync(turmaid);

            var model = new FaltaDisciplinaViewModel
            {
                turmaid = turma.Id,
                nometurma = turma.Nome,
                disciplinaid = disciplinaid,
                Disciplinas = _disciplinasRepository.GetComboDisciplinasporTurmaAsync(turma.Id)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateDisciplinaFaltas(FaltaDisciplinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateAlunoFaltas", "Faltas", model);
            }
            model.Disciplinas = _disciplinasRepository.GetComboDisciplinasporTurmaAsync(model.turmaid);
            return View(model);
        }

        public async Task<IActionResult> CreateAlunoFaltas(FaltaDisciplinaViewModel model2)
        {
            if (ModelState.IsValid)
            {
                var disciplina = await _disciplinasRepository.GetByIdAsync(model2.disciplinaid);

                var model = new FaltaAlunoTurmaDisciplinaViewModel
                {
                    turmaid = model2.turmaid,
                    nometurma = model2.nometurma,
                    disciplinaid = model2.disciplinaid,
                    nomedisciplina = disciplina.Nome,
                    duracao = disciplina.Duracao,
                    FaltaAlunos = (await _faltaRepository.GetFaltasTurmaAluno(model2.turmaid, model2.disciplinaid)).ToList()
                };
                return View(model);
            }

            return RedirectToAction("CreateTurmaFaltas", "Faltas");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlunoFaltas(FaltaAlunoTurmaDisciplinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var turma = await _turmasRepository.GetByIdAsync(model.turmaid);
                var disciplina = await _disciplinasRepository.GetByIdAsync(model.disciplinaid);
                int soma = 0;
                int totalduracao = 0;
                try
                {
                    foreach (var aluno in model.FaltaAlunos)
                    {
                        
                        if (aluno.duracao != 0)
                        {
                            soma = aluno.duracao + aluno.horasfalta;
                            totalduracao = model.duracao - aluno.horasfalta; 
                            if (model.duracao > aluno.horasfalta)
                            {
                                if(soma > model.duracao)
                                {
                                    await _faltaRepository.CreateAsync(new Falta
                                    {
                                        alunoid = aluno.alunoid,
                                        disciplinaid = model.disciplinaid,
                                        Data = model.Data,
                                        duracao = totalduracao
                                        
                                    });
                                }else 
                                {
                                    await _faltaRepository.CreateAsync(new Falta
                                    {
                                        alunoid = aluno.alunoid,
                                        disciplinaid = model.disciplinaid,
                                        Data = model.Data,
                                        duracao = aluno.duracao
                                    });
                                }

                            }
                            {
                                ViewBag.Message = " Máximo de horas de disciplina excedidas";
                            }
                           
                        }

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.message = ex.Message;
                }

                var modelparaview = new FaltaDisciplinaViewModel
                {
                    disciplinaid = model.disciplinaid,
                    nometurma = model.nometurma,
                    turmaid = model.turmaid
                };



                return RedirectToAction("CreateAlunoFaltas", "Faltas", modelparaview);
            }
            return View(model);
        }

    }
}
