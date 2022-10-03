using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class NotaRepository : GenericRepository<Nota>, INotaRepository
    {
        private readonly DataContext _context;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly ITurmasRepository _turmasRepository;

        public NotaRepository(DataContext context,IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository) : base(context)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _turmasRepository = turmasRepository;
        }

        public async Task<Nota> GetNota(int turmaid, int disciplinaid)
        {
            return await _context.Nota.Where(p => p.id_turma == turmaid && p.id_disciplina == disciplinaid).FirstOrDefaultAsync();
        }

        public IQueryable<NotaViewModel> NotasToNotasViewModels(IQueryable<Nota> notas)
        {
            return notas.Select(nota => new NotaViewModel
            {

                id_aluno = nota.id_aluno,
                aluno = nota.aluno,
                Anotação = nota.Anotação,
                NotaAluno = nota.NotaAluno,
                Data = nota.Data,
                disciplina = nota.disciplina,
                id_disciplina = nota.id_disciplina,
                id_turma = nota.id_turma,
                Turma = nota.Turma


            }); ;
        }
    }
}
