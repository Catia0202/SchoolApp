using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface ITurmasRepository : IGenericRepository<Turma>
    {
        IEnumerable<SelectListItem> GetComboTurmas();
        Task<IQueryable<Turma>> GetIndexTurmasAsync();

        
    }
}
