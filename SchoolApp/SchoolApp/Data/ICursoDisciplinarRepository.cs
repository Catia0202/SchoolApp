using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface ICursoDisciplinarRepository : IGenericRepository<CursoDisciplina>
    {
      public Task<CursoDisciplina> GetTurmaDisciplinaAsync(int turmaid, int disciplinaid);

        Task<IQueryable<CursoDisciplina>> GetIndexTurmasDisciplinasAsync(int turmaid);

       
    }
}
