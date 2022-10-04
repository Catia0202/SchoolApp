using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface INotaRepository : IGenericRepository<Nota>
    {
        Task<Nota> GetNota(int turmaid, int disciplinaid);

          
        IQueryable<NotaViewModel> NotasToNotasViewModels(IQueryable<Nota> notas);
    }
}
