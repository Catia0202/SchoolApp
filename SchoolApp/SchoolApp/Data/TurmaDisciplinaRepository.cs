using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class TurmaDisciplinaRepository : GenericRepository<TurmaDisciplina> , ITurmaDisciplinarRepository
    {
        private readonly DataContext _context;

        public TurmaDisciplinaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TurmaDisciplina> GetTurmaDisciplinaAsync(int turmaid, int disciplinaid)
        {
            return await _context.turmaDisciplina.Where(x => x.TurmaId == turmaid && x.DisciplinaId == disciplinaid)
             .FirstOrDefaultAsync();
        }
    }
}
