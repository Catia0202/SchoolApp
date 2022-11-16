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
    public class NotaRepository : GenericRepository<Nota>, INotaRepository
    {
        private readonly DataContext _context;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly ITurmasRepository _turmasRepository;
        private readonly ICursoDisciplinarRepository _turmaDisciplinarRepository;

        public NotaRepository(DataContext context,IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository, ICursoDisciplinarRepository turmaDisciplinarRepository) : base(context)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _turmasRepository = turmasRepository;
            _turmaDisciplinarRepository = turmaDisciplinarRepository;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboTurmasporAlunoAsync(int alunoid)
        {
            var list = Enumerable.Empty<SelectListItem>();

            await Task.Run(() =>
            {
                list = (
                from curso in _context.Cursos
                join turma in _context.Turmas
                on curso.Id equals turma.CursoId
                join aluno in _context.Alunos
                on turma.Id equals aluno.turmaid
                where aluno.Id == aluno.Id
                select new
                {
                    CursoId = curso.Id,
                    Nome = curso.Nome
                }).Select(x => new SelectListItem
                {
                    Value = x.CursoId.ToString(),
                    Text = x.Nome
                });
            });
             return list;
          
        }

        public async Task<Nota> GetNotaByDados(int alunoid, int turmaid, int disciplinaid)
        {
            return await _context.Notas.Where(p => p.idaluno == alunoid && p.idturma == turmaid && p.iddisciplina == disciplinaid).FirstOrDefaultAsync();
        }

        public async Task<IList<NotaAlunoViewModel>> GetNotasAlunoAsync()
        {
            var alunos = new List<NotaAlunoViewModel>();
            await Task.Run(() =>
            {
                alunos =  _context.Alunos.Select(p => new NotaAlunoViewModel
            {
              
                alunoId = p.Id,
                Nome = p.Nome,
                Foto = p.ImageUrl
            }).ToList();




            });
            return alunos;
        }

 
        public async Task<IEnumerable<NotaViewModel>> GetNotasAlunoDaTurma(int alunoid ,int cursoid)
        {
            var alunos = Enumerable.Empty<NotaViewModel>();
    
            var disciplina = _turmaDisciplinarRepository.GetAll().Where(p => p.CursoId == cursoid).ToList();

       

            await Task.Run(() =>
            {


                alunos = _context.Notas.Include(p => p.disciplina).Where(p => p.idaluno == alunoid && p.idturma == cursoid).Select(p => new NotaViewModel
                {
                    DisciplinaId = p.iddisciplina,
                    NomeDisciplina = _disciplinasRepository.GetAll().Where(x => x.Id == p.iddisciplina).Select(x => x.Nome).FirstOrDefault().ToString(),
                    Data = (System.DateTime)p.Data,
                    Nota = p.NotaAluno



                }).ToList();

            });
            return alunos;
        }

        public async Task<IQueryable<NotaAlunoCreateViewModel>> GetNotasAlunoDaTurmaDisciplina(int turmaid, int disciplinaid)
        {

            var alunos = Enumerable.Empty<NotaAlunoCreateViewModel>().AsQueryable();

            await Task.Run(() =>
            {
                alunos = (from alunos in _context.Alunos
                          where alunos.turmaid == turmaid
                          orderby alunos.Nome
                          select new
                          {
                              alunoid = alunos.Id,
                              nome = alunos.Nome,
                              foto = alunos.ImageUrl,
                              Nota = (
                              from notas in _context.Notas
                              where
                                    notas.idaluno == alunos.Id && notas.idturma == turmaid && notas.iddisciplina == disciplinaid
                              select new
                              {
                                  Data = notas.Data,
                                  nota = notas.NotaAluno
                              }).First()
                          }).Select(p => new NotaAlunoCreateViewModel
                          {
                              alunoid = p.alunoid,
                              nome = p.nome,
                              foto = p.foto,
                              Data = p.Nota.Data,
                              nota = p.Nota.nota
                          });


            });
              return alunos;
        }
    }
}
