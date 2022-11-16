using Microsoft.AspNetCore.Mvc.Rendering;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class TurmasRepository : GenericRepository<Turma>, ITurmasRepository
    {
        private readonly DataContext _context;

        public TurmasRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboTurmas()
        {
            var list = _context.Turmas.Select(p => new SelectListItem {
                Text = p.Nome,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem {
                Text = "(Selecione uma turma...)",
                Value = "0"
            });
            return list;
        }

        public async Task<IQueryable<Turma>> GetIndexTurmasAsync()
        {
           var turmas = Enumerable.Empty<Turma>().AsQueryable();

            await Task.Run(() =>
            {
                turmas = _context.Turmas.OrderBy(x => x.Nome).Select(x => new Turma
                {
                    Id = x.Id,
                    CursoId =x.CursoId,
                    Nome = x.Nome,
                    DataInicio = x.DataInicio,
                    DataFim = x.DataFim
                    
                });
            });
            return turmas;
        }
    }
}
