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
    public class NotasController : Controller
    {
        
        private readonly INotaRepository _notaRepository;
        private readonly IAlunosRepository _alunosRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly IDisciplinasRepository _disciplinasRepository;

        public NotasController(INotaRepository notaRepository, IAlunosRepository alunosRepository, IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository)
        {
         
            _notaRepository = notaRepository;
            _alunosRepository = alunosRepository;
            _turmasRepository = turmasRepository;
            _disciplinasRepository = disciplinasRepository;
        }

        // GET: Notas
        public IActionResult Index(string search)
        {

            
            var model = Enumerable.Empty<NotaViewModel>();
            var notas = _notaRepository.GetAll().Include(p => p.Turma);

            
          

            if (notas.Any())
            {
                model = (_notaRepository.NotasToNotasViewModels(notas).OrderBy(x => x.aluno.Nome));
               
            }
            else
            {
                ViewBag.message = "Não foram encontradas notas";

            }
            return View(model);
            //if (!String.IsNullOrEmpty(search))
            //{
            //    notas = (IOrderedQueryable<Nota>)notas.Where(a => a.Id == int.Parse(search));

            //}

        }

        //    // GET: Notas/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var nota = await _context.Nota
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (nota == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(nota);
        //    }

        // GET: Notas/Create
        public IActionResult Create()
        {
            var model = new NotaViewModel
            {
                Alunos = _alunosRepository.GetListAlunos(),
                Turmas = _turmasRepository.GetComboTurmas(),
                Disciplinas = _disciplinasRepository.GetListDisciplinas()
            };

            return View(model);
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Turmas = _turmasRepository.GetComboTurmas();
                model.Alunos = _alunosRepository.GetListAlunos();
                model.Disciplinas = _disciplinasRepository.GetListDisciplinas();
               
               
                Nota nota = new Nota()
                {

                    Id = model.Id,
                    Anotação = model.Anotação,
                    id_aluno = model.AlunoId,
                    NotaAluno = model.NotaAluno,
                    Data = model.Data,
                    id_disciplina = model.DisciplinaId,
                    id_turma = model.TurmaId
                };


                await _notaRepository.CreateAsync(nota);
   
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //    // GET: Notas/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var nota = await _context.Nota.FindAsync(id);
        //        if (nota == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(nota);
        //    }

        //    // POST: Notas/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,NotaAluno,Anotação,id_aluno,id_turma,id_disciplina,Data")] Nota nota)
        //    {
        //        if (id != nota.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(nota);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!NotaExists(nota.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(nota);
        //    }

        //    // GET: Notas/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var nota = await _context.Nota
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (nota == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(nota);
        //    }

        //    // POST: Notas/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var nota = await _context.Nota.FindAsync(id);
        //        _context.Nota.Remove(nota);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool NotaExists(int id)
        //    {
        //        return _context.Nota.Any(e => e.Id == id);
        //    }
    }
}
