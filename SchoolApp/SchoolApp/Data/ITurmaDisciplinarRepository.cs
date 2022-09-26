using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface ITurmaDisciplinarRepository : IGenericRepository<TurmaDisciplina>
    {
      public Task<TurmaDisciplina> GetTurmaDisciplinaAsync(int turmaid, int disciplinaid);
    }
}
