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

        public NotaRepository(DataContext context,IDisciplinasRepository disciplinasRepository, ITurmasRepository turmasRepository) : base(context)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _turmasRepository = turmasRepository;
        }

        public  IEnumerable<SelectListItem> GetComboTurmasporAlunoAsync(int alunoid)
        {
            var list = new List<SelectListItem>();

            var turma = _context.Aluno.Include(p => p.turma).Where(p => p.Id == alunoid).FirstOrDefault() ;

       
            list.Insert(0, new SelectListItem
            {



                Text = turma.Nome,
                Value = turma.turmaid.ToString()
            }); ;
            return list;
          
        }

        public async Task<Nota> GetNotaByDados(int alunoid, int turmaid, int disciplinaid)
        {
            return await _context.Nota.Where(p => p.idaluno == alunoid && p.idturma == turmaid && p.iddisciplina == disciplinaid).FirstOrDefaultAsync();
        }

        public async Task<IList<NotaAlunoViewModel>> GetNotasAlunoAsync()
        {
            var alunos = new List<NotaAlunoViewModel>();
            await Task.Run(() =>
            {
                alunos =  _context.Aluno.Select(p => new NotaAlunoViewModel
            {
                alunoId = p.Id,
                Nome = p.Nome,
                Foto = p.ImageUrl
            }).ToList();




            });
            return alunos;
        }

         


        public async Task<IEnumerable<NotaViewModel>> GetNotasAlunoDaTurma(int alunoid ,int turmaid)
        {
            var alunos = Enumerable.Empty<NotaViewModel>();
            var disciplinas = _disciplinasRepository.GetAll().ToList();

     
            await Task.Run(() =>
            {
                alunos = _context.Nota.Include(p => p.disciplina).Where(p => p.idaluno == alunoid && p.idturma == turmaid).Select(p => new NotaViewModel
                {
                    DisciplinaId = p.iddisciplina,
                    NomeDisciplina = p.disciplina.Nome,
                    Data = p.Data,
                    Nota = p.NotaAluno,
                   


                });
            });
            return alunos;
        }

        public async Task<IQueryable<NotaAlunoCreateViewModel>> GetNotasAlunoDaTurmaDisciplina(int turmaid, int disciplinaid)
        {

            var alunos = Enumerable.Empty<NotaAlunoCreateViewModel>().AsQueryable();

            await Task.Run(() =>
            {
                alunos = (from alunos in _context.Aluno
                          where alunos.turmaid == turmaid
                          orderby alunos.Nome
                          select new
                          {
                              alunoid = alunos.Id,
                              nome = alunos.Nome,
                              foto = alunos.ImageUrl,
                              Nota = (
                              from notas in _context.Nota
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
