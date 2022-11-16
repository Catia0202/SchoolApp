using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface ICursoRepository :IGenericRepository<Curso>
    {
        IEnumerable<SelectListItem> GetComboCursos();
        Task<IQueryable<Curso>> GetIndexCursoAsync();
    }
}
