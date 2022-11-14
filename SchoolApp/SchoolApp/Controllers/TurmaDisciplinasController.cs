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
    public class TurmaDisciplinasController : Controller
    {
        private readonly DataContext _context;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly ITurmaDisciplinarRepository _turmaDisciplinarepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;

        public TurmaDisciplinasController(DataContext context, IDisciplinasRepository disciplinasRepository,ITurmasRepository turmasRepository, ITurmaDisciplinarRepository turmaDisciplinaRepository, IConfiguracaoRepository configuracaoRepository)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _turmasRepository = turmasRepository;
            _turmaDisciplinarepository = turmaDisciplinaRepository;
            _configuracaoRepository = configuracaoRepository;
        }

        // GET: TurmaDisciplinas
        public IActionResult Index(int id)
        {
           
          var dataContext = _context.turmaDisciplina.Include(t => t.Disciplina).Include(t => t.turma).Where(t => t.TurmaId == id);
            if(id != 0)
            {
                ViewBag.data = id;
            }
   
            
            return View(dataContext);
        }

        // GET: TurmaDisciplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaDisciplina = await _context.turmaDisciplina
                .Include(t => t.Disciplina)
                .Include(t => t.turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turmaDisciplina == null)
            {
                return NotFound();
            }

            return View(turmaDisciplina);
        }

        // GET: TurmaDisciplinas/Create
        public IActionResult Create()
        {
           TurmaDisciplinaViewModel  disciplinas = new TurmaDisciplinaViewModel();
            disciplinas.listdisciplinas = _disciplinasRepository.GetListDisciplinas();
            ViewBag.DataSource = disciplinas;
            return View(disciplinas);

         
        }

        // POST: TurmaDisciplinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaDisciplinaViewModel model, int id)
        {

            var turma = await _turmasRepository.GetByIdAsync(id);
            model.listdisciplinas = _disciplinasRepository.GetListDisciplinas();
            var adminconfig = _configuracaoRepository.GetAll().FirstOrDefaultAsync();
            int horastotais = 0;
            int disciplinasjanaturma = _turmaDisciplinarepository.GetAll().Where(p => p.TurmaId == id).Count(); 
            List<SelectListItem> selectListItems = model.listdisciplinas.Where(p => model.disciplinaids.Contains(int.Parse(p.Value))).ToList();
            var disciplinasselecionadas = selectListItems.Count();
            foreach(var item in selectListItems)
            {
                var disciplinaselecionada = _disciplinasRepository.GetByIdAsync(int.Parse(item.Value));
                horastotais = horastotais + disciplinaselecionada.Result.Duracao;
            }
            
            foreach (var selectListItem in selectListItems)
            {
                selectListItem.Selected = true;
                ViewBag.message += "\\n" + selectListItem.Text;
                
            
                    var turmadisciplina = await _turmaDisciplinarepository.GetTurmaDisciplinaAsync(id, int.Parse(selectListItem.Value));
                  

                
                if(adminconfig.Result.MaximoDisciplinasPorTurma > disciplinasselecionadas && adminconfig.Result.MaximoDisciplinasPorTurma > disciplinasjanaturma)
                {
                    if (horastotais <= turma.Duracao)
                    {
                        if (turmadisciplina == null && selectListItem.Selected == true)
                        {
                            await _turmaDisciplinarepository.CreateAsync(new TurmaDisciplina
                            {
                                DisciplinaId = int.Parse(selectListItem.Value),
                                TurmaId = id
                            });
                        }

                    }
                    else
                    {
                        ViewBag.errormessage = "Não foi possível adicionar essas disciplinas ao Curso devido à soma das horas das Disciplinas ser maior que a Duração do Curso";
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

            var turmaDisciplina = await _context.turmaDisciplina
                .Include(t => t.Disciplina)
                .Include(t => t.turma)
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
            var turmaDisciplina = await _context.turmaDisciplina.FindAsync(id);
            _context.turmaDisciplina.Remove(turmaDisciplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaDisciplinaExists(int id)
        {
            return _context.turmaDisciplina.Any(e => e.Id == id);
        }
    }
}
