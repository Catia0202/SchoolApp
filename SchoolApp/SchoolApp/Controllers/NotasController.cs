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
        private readonly IUserHelper _userHelper;
        private readonly IDisciplinasRepository _disciplinasRepository;

        public NotasController(INotaRepository notaRepository, IAlunosRepository alunosRepository, IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository, IUserHelper userHelper)
        {

            _notaRepository = notaRepository;
            _alunosRepository = alunosRepository;
            _turmasRepository = turmasRepository;
            _userHelper = userHelper;
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
                    aluno.Turmas = _notaRepository.GetComboTurmasporAlunoAsync(aluno.alunoId);
                }
            }

            return View(alunos);
        }
        
        [HttpPost]
        public IActionResult Index( int AlunoId)
        {
            var model = new NotaAlunoViewModel();
            if (ModelState.IsValid)
            {
                var aluno = _alunosRepository.GetAll().Where(p => p.Id == AlunoId);

                var turmaid = aluno.FirstOrDefault().turmaid;

              
               model = new NotaAlunoViewModel
                {
                    alunoId = aluno.FirstOrDefault().Id,
                    Foto = aluno.FirstOrDefault().ImageUrl,
                    Nome=aluno.FirstOrDefault().Nome,
                    TurmaId = turmaid
                };
                return RedirectToAction("IndexNotasAluno", "Notas", new { AlunoId, turmaid });
            } 
            
            return View(model);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> IndexNotasAluno(int AlunoId, int turmaid)
        {
            if (AlunoId == 0)
            {
                return RedirectToAction("Index", "Notas");
            }

            var aluno = await _alunosRepository.GetByIdAsync(AlunoId);
            var turma = await _turmasRepository.GetByIdAsync(turmaid);
           

            
            var model = new TodasNotasDoAlunoViewModel
            {
                Nome = aluno.Nome,
                foto = aluno.ImageUrl,
               Turma= turma.Nome,
                Notas = await _notaRepository.GetNotasAlunoDaTurma(aluno.Id, turma.Id)
            };



            return View(model);
        }
        [Authorize(Roles = "Funcionario")]
        public IActionResult CreateTurmaNota(int turmaid)
        {
            var model = new NotaTurmaViewModel
            {
                turmaid = turmaid,
                Turmas = _turmasRepository.GetComboTurmas()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateTurmaNota(NotaTurmaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateDisciplinaNota", "Notas", new { turmaid = model.turmaid });
            }
            model.Turmas = _turmasRepository.GetComboTurmas();
            return View(model);
        }
        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> CreateDisciplinaNota(int turmaid, int disciplinaid)
        {
            if(turmaid== 0)
            {
                return RedirectToAction("CreateTurmaNota", "Notas");
            }

            var turma = await _turmasRepository.GetByIdAsync(turmaid);

            var model = new NotaDisciplinaViewModel
            {
              turmaid= turma.Id,
              nometurma = turma.Nome,
                disciplinaid = disciplinaid,
                Disciplinas =  _disciplinasRepository.GetComboDisciplinasporTurmaAsync(turma.Id)
            };
            return View(model);
        }
    
        [HttpPost]
        public IActionResult CreateDisciplinaNota(NotaDisciplinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CreateAlunoNota", "Notas", model);
            }
            model.Disciplinas = _disciplinasRepository.GetComboDisciplinasporTurmaAsync(model.turmaid);
            return View(model);
        }

        [Authorize(Roles = "Funcionario")]
        public async Task<IActionResult> CreateAlunoNota(NotaDisciplinaViewModel model2)
        {
            if (ModelState.IsValid)
            {
                var disciplina = await _disciplinasRepository.GetByIdAsync(model2.disciplinaid);

                var model = new NotaAlunoTurmaDisciplinaViewModel
                {
                    disciplinaId = model2.disciplinaid,
                    disciplinaNome = disciplina.Nome,
                    TurmaId = model2.turmaid,
                    turmaNome = model2.nometurma,
                    Duracao = disciplina.Duracao,
                    Alunos = (await _notaRepository.GetNotasAlunoDaTurmaDisciplina(model2.turmaid, model2.disciplinaid)).ToList()
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
                                    idaluno =alunos.Id,
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
                    disciplinaid = model.disciplinaId,
                    nometurma = model.turmaNome,
                    turmaid = model.TurmaId
                };



                return RedirectToAction("CreateAlunoNota", "Notas", modelparaview);
            }
            return View(model);
        }




    }
}
