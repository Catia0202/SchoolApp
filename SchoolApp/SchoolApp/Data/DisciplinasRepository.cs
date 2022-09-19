using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class DisciplinasRepository : GenericRepository<Disciplina>, IDisciplinasRepository
    {
        private readonly DataContext _context;
        public DisciplinasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        //public IEnumerable<SelectListItem> GetListDisciplinasNaTurma(int turmaId)
        //{
        //    var list = _context.turmaDisciplina.Include(x => x.Disciplina).Where(x => x.TurmaId == turmaId).Select(p => new SelectListItem
        //    {
        //        Text = p.Disciplina.Nome,
        //        Value = p.Id.ToString()
        //    }).ToList();

        //    list.Insert(0, new SelectListItem
        //    {
        //        Text = "(Selecione uma disciplina...)",
        //        Value = "0"
        //    });
        //    return list;
        //}

    }
}
