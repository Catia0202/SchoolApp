using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class AlunosRepository : GenericRepository<Aluno> , IAlunosRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AlunosRepository(DataContext context,IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Aluno.Include(p => p.User);
        }

        public async Task<AlunoViewModel> GetAlunoByIdWithTurmaAsync(int id)
        {
            var aluno = await _context.Aluno.Include(x => x.turma).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (aluno == null)
            {
                return null;
            }
            return new AlunoViewModel
            {
                Id = aluno.Id,
                ImageUrl = aluno.ImageUrl,
                Nome = aluno.Nome,
                Data_Nascimento = aluno.Data_Nascimento,
                Email = aluno.Email,
                Genero = aluno.Genero,
                Morada = aluno.Morada,
                Telemovel = aluno.Telemovel,
                User = aluno.User,
                turmaid = aluno.turmaid,
                turma = aluno.turma
                //Turmas = _turmasRepository.GetComboTurmas()
            };
        }

        public async Task<IQueryable<AvaliacaoAlunoDisciplinaViewModel>> GetAvaliacaoAlunoEmDisciplinaAsync(int alunoid, int turmaid)
        {
            var avaliacao = Enumerable.Empty<AvaliacaoAlunoDisciplinaViewModel>().AsQueryable();

            await Task.Run(() =>
            {
                avaliacao = (
                from user in _context.Users
                join aluno in _context.Aluno
                on user.Id equals aluno.User.Id
                join nota in _context.Nota
                on aluno.Id equals nota.idaluno
               
                join turma in _context.turma
                on aluno.turmaid equals turma.Id
             
                join turmadisciplina in _context.turmaDisciplina
                on turma.Id equals turmadisciplina.TurmaId
                join disciplina in _context.Disciplina
                on turmadisciplina.DisciplinaId equals disciplina.Id
                join falta in _context.falta
                on disciplina.Id equals falta.disciplinaid
                where aluno.Id == alunoid && turma.Id == turmaid
                select new
                {
                    DisciplinId = disciplina.Id,
                    disciplinNome = disciplina.Nome,
                    disciplinDur = disciplina.Duracao,
                    horasfalta = (
                    from falta in _context.falta
                    where falta.alunoid == aluno.Id && falta.disciplinaid == disciplina.Id
                    select falta.duracao).Sum(),
                    horasdisciplina = (
                    from disciplina in _context.Disciplina
                    where disciplina.Id == disciplina.Id
                    select disciplina.Duracao).FirstOrDefault(),
                    nota = (
                    from nota in _context.Nota
                    where nota.idaluno == aluno.Id  && nota.iddisciplina == disciplina.Id && nota.idturma == turma.Id
                    select new
                    {
                        data = nota.Data,
                        nota = nota.NotaAluno
                    }).First(),
                }).Select(p => new AvaliacaoAlunoDisciplinaViewModel
                {
                    Iddisciplina = p.DisciplinId,
                    Nomedisciplina = p.disciplinNome,
                    Duracaodisciplina = p.disciplinDur,
                    Horasfalta = p.horasfalta,
                    Nota = p.nota.nota,
                    DataNota = p.nota.data,
                    Chumbounota = p.nota.nota < 10,
                    Percentagemfalta = CalculaPercentagem(p.horasfalta, p.horasdisciplina),
                    ChumbouFaltas = CalculaPercentagem(p.horasfalta, p.horasdisciplina) >= 10 ? true : false

                }).Distinct();




            });

            return avaliacao;
        }

            private static int CalculaPercentagem(int horasfalta, int horasdiciplina)
            {
                if(horasdiciplina ==  0 && horasfalta == 0)
                {
                    return 0;
                }
                double total = Convert.ToDouble(horasdiciplina);
                double faltas = Convert.ToDouble(horasfalta);
                double percentagem = 100/(total/faltas);

                return Convert.ToInt32(percentagem);
            }
            
            
            
            
            
            
            
      
        public List<SelectListItem> GetListAlunos()
        {
            var list = _context.Aluno.ToList();
            List<SelectListItem> lista = new List<SelectListItem>();
            foreach (var item in list)
            {
                lista.Add(new SelectListItem
                {
                    Text = item.Nome,
                    Value = item.Id.ToString()

                });
            }
            return lista;
        }



        //public async Task<IQueryable<Turma>> GetTurmasAsync(string username)
        //{
        //  var user =await _userHelper.GetUserByEmailAsync(username);
        //    if(user== null)
        //    {
        //        return null;
        //    }

        //        return _context.Aluno.Include(o => o.turma).OrderBy(o => o.turma.Nome);


        //}
    }
}
