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
    public class CursoDisciplinasController : Controller
    {
        private readonly DataContext _context;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly ICursoDisciplinarRepository _cursoDisciplinarepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;

        public CursoDisciplinasController(DataContext context, IDisciplinasRepository disciplinasRepository,ITurmasRepository turmasRepository, ICursoDisciplinarRepository cursoDisciplinaRepository, IConfiguracaoRepository configuracaoRepository)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _turmasRepository = turmasRepository;
            _cursoDisciplinarepository = cursoDisciplinaRepository;
            _configuracaoRepository = configuracaoRepository;
        }

        
        public IActionResult Index(int id)
        {
           
          var cursoDisciplinas = _context.CursoDisciplinas.Include(t => t.Disciplina).Include(t => t.Curso).Where(t => t.CursoId == id);
            if(id != 0)
            {
                ViewBag.data = id;
            }
   
            
            return View(cursoDisciplinas);
        }

     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoDisciplina = await _context.CursoDisciplinas
                .Include(t => t.Disciplina)
                .Include(t => t.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoDisciplina == null)
            {
                return NotFound();
            }

            return View(cursoDisciplina);
        }

      
        public IActionResult Create()
        {
           CursoDisciplinaViewModel  disciplinas = new CursoDisciplinaViewModel();
            disciplinas.listdisciplinas = _disciplinasRepository.GetListDisciplinas();
            ViewBag.DataSource = disciplinas;
            return View(disciplinas);

         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CursoDisciplinaViewModel model, int id)
        {
            model.listdisciplinas = _disciplinasRepository.GetListDisciplinas();
         
            var adminconfig = _configuracaoRepository.GetAll().FirstOrDefaultAsync();
          
            List<SelectListItem> selectListItems = model.listdisciplinas.Where(p => model.disciplinaids.Contains(int.Parse(p.Value))).ToList();
            
            foreach(var item in selectListItems)
            {
                var disciplinaselecionada = _disciplinasRepository.GetByIdAsync(int.Parse(item.Value));
                
            }
            int disciplinasjanaturma = _cursoDisciplinarepository.GetAll().Where(p => p.CursoId == id).Count();
            foreach (var selectListItem in selectListItems)
            {
                var disciplinasselecionadas = selectListItems.Count();
               
                selectListItem.Selected = true;
                ViewBag.message += "\\n" + selectListItem.Text;
                
            
                    var turmadisciplina = await _cursoDisciplinarepository.GetTurmaDisciplinaAsync(id, int.Parse(selectListItem.Value));
                  

                
                if(adminconfig.Result.MaximoDisciplinasPorTurma > disciplinasselecionadas  && adminconfig.Result.MaximoDisciplinasPorTurma > disciplinasjanaturma)
                {
                    if (turmadisciplina == null && selectListItem.Selected == true)
                        {
                            await _cursoDisciplinarepository.CreateAsync(new CursoDisciplina
                            {
                                DisciplinaId = int.Parse(selectListItem.Value),
                                CursoId = id
                            });
                        }

                    
                 
                }
                else
                {
                    ViewBag.errormessage = "Não foi possível adicionar essas disciplinas ao Curso devido a ter excedido o número de disciplinas neste curso";
                }
               

            };

           
            return View(model);
        }
    

           
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var turmaDisciplina = await _context.turmaDisciplina.FindAsync(id);
        //    if (turmaDisciplina == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id", "Id", turmaDisciplina.DisciplinaId);
        //    ViewData["TurmaId"] = new SelectList(_context.turma, "Id", "Id", turmaDisciplina.TurmaId);
        //    return View(turmaDisciplina);
        //}

        // POST: TurmaDisciplinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TurmaId,DisciplinaId")] TurmaDisciplina turmaDisciplina)
        //{
        //    if (id != turmaDisciplina.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(turmaDisciplina);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TurmaDisciplinaExists(turmaDisciplina.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DisciplinaId"] = new SelectList(_context.Disciplina, "Id", "Id", turmaDisciplina.DisciplinaId);
        //    ViewData["TurmaId"] = new SelectList(_context.turma, "Id", "Id", turmaDisciplina.TurmaId);
        //    return View(turmaDisciplina);
        //}

        // GET: TurmaDisciplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaDisciplina = await _context.CursoDisciplinas
                .Include(t => t.Disciplina)
                .Include(t => t.Curso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turmaDisciplina == null)
            {
                return NotFound();
            }

            return View(turmaDisciplina);
        }

        // POST: TurmaDisciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turmaDisciplina = await _context.CursoDisciplinas.FindAsync(id);
            _context.CursoDisciplinas.Remove(turmaDisciplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
