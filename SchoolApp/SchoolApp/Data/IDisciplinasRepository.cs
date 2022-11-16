using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface IDisciplinasRepository : IGenericRepository<Disciplina>
    {
      public List<SelectListItem> GetListDisciplinas();

        Task<IEnumerable<SelectListItem>> GetComboDisciplinasporCursoAsync(int turmaid);

        Task<IQueryable<Disciplina>> GetIndexTurmasDisciplinasAsync(int turmaid);
    }
}
