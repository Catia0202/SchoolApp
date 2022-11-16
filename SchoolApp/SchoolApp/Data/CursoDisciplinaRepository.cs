using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class CursoDisciplinaRepository : GenericRepository<CursoDisciplina> , ICursoDisciplinarRepository
    {
        private readonly DataContext _context;

        public CursoDisciplinaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CursoDisciplina> GetTurmaDisciplinaAsync(int cursoid, int disciplinaid)
        {
            return await _context.CursoDisciplinas.Where(x => x.CursoId == cursoid && x.DisciplinaId == disciplinaid)
             .FirstOrDefaultAsync();
        }
      

        public async Task<IQueryable<CursoDisciplina>> GetIndexTurmasDisciplinasAsync(int idcurso)
        {
            var turmas = Enumerable.Empty<CursoDisciplina>().AsQueryable();

            await Task.Run(() =>
            {
                turmas = _context.CursoDisciplinas.Include(p => p.Disciplina).Where(x => x.CursoId == idcurso).Select(x => new CursoDisciplina
                {
                   Curso= x.Curso
                });
            });
            return turmas;
        }


    }
}
