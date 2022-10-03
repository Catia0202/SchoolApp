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

        public NotasController(INotaRepository notaRepository )
        {
            
            _notaRepository = notaRepository;
        }

        // GET: Notas
        public IActionResult Index(string search)
        {
            var model = Enumerable.Empty<NotaViewModel>();
            var notas = _notaRepository.GetAll().Include(p=> p.Turma).Include(p => p.disciplina).Include(p=> p.aluno);

            if (notas.Any())
            {
                model = (_notaRepository.NotasToNotasViewModels(notas).OrderBy(x => x.aluno.Nome));
            }
            else
            {
                ViewBag.message = "Não foram encontrados alunos";
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
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NotaAluno,Anotação,id_aluno,id_turma,id_disciplina,Data")] Nota nota)
        {
            if (ModelState.IsValid)
            {
               
                await _notaRepository.CreateAsync(nota);
                return RedirectToAction(nameof(Index));
            }
            return View(nota);
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
