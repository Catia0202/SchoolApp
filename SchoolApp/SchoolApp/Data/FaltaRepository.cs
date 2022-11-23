using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class FaltaRepository : GenericRepository<Falta> , IFaltaRepository
    {
        private readonly DataContext _context;
        private readonly IDisciplinasRepository _disciplinasRepository;
        private readonly IConfiguracaoRepository _configuracaoRepository;

        public FaltaRepository(DataContext context, IDisciplinasRepository disciplinasRepository, IConfiguracaoRepository configuracaoRepository):base(context)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
            _configuracaoRepository = configuracaoRepository;
        }

        public async Task<IList<FaltaAlunoViewModel>> GetFaltasAlunoAsync()
        {
            var alunos = new List<FaltaAlunoViewModel>();
            await Task.Run(() =>
            {
                alunos = _context.Alunos.Select(p => new FaltaAlunoViewModel
                {
                    alunoId = p.Id,
                    Nome = p.PrimeiroNome + " "+ p.UltimoNome,
                    Foto = p.ImageUrl
                }).ToList();
            });
            return alunos;
        }

        public IEnumerable<SelectListItem> GetComboTurmasporAlunoAsync(int alunoid)
        {
            var list = new List<SelectListItem>();

            var turma = _context.Alunos.Include(p => p.turma).Where(p => p.Id == alunoid).FirstOrDefault();


            list.Insert(0, new SelectListItem
            {
                Text = turma.turma.Nome,
                Value = turma.turmaid.ToString()
            }); ;
            return list;

        }

        public async Task<IEnumerable<FaltaViewModel>> GetFaltasAlunoDaTurma(int alunoid,int disciplinaid)
        {
            var alunos = Enumerable.Empty<FaltaViewModel>();
            var disciplinas = _disciplinasRepository.GetAll().ToList();
            var configuracao = await _configuracaoRepository.GetAll().FirstOrDefaultAsync();

            await Task.Run(() =>
            {
                alunos = (from alunos in _context.Alunos
                          where alunos.Id == alunoid
                          orderby alunos.PrimeiroNome
                          select new
                          {
                              alunoid = alunos.Id,
                              nome = alunos.PrimeiroNome,
                              foto = alunos.ImageUrl,
                              horasdisciplinas = (
                                  from disciplina in _context.Disciplinas
                                  where disciplina.Id == disciplinaid
                                  select disciplina.Duracao 
                             ).FirstOrDefault(),
                              horasfalta = (
                              from Falta in _context.Faltas
                              where
                                    Falta.alunoid == alunos.Id && Falta.disciplinaid == disciplinaid
                              select Falta.duracao).Sum(),
                              nomedisciplina =(
                              from disciplina in _context.Disciplinas
                              where disciplina.Id == disciplinaid
                              select disciplina.Nome
                              ).ToList(),
                          }).Select(p => new FaltaViewModel
                          {
                              NomeDisciplina = p.nomedisciplina.ToString(),
                              alunoid = p.alunoid,
                              nome = p.nome,
                              foto = p.foto,
                              horasfalta = p.horasfalta,
                              percentagem = Percentagem(p.horasfalta, p.horasdisciplinas),
                              excluido = Percentagem(p.horasfalta, p.horasdisciplinas) >= configuracao.PercentagemdeFaltas /100 ? true : false

                          });


            
            });
            return alunos;
        }

        public async Task<IQueryable<FaltaAluno>> GetFaltasTurmaAluno(int turmaid, int disciplinaid)
        {
            var alunos = Enumerable.Empty<FaltaAluno>().AsQueryable();

            await Task.Run(() =>
            {   
                alunos = (from alunos in _context.Alunos
                          where alunos.turmaid == turmaid
                          orderby alunos.PrimeiroNome
                          select new
                          {
                              alunoid = alunos.Id,
                              nome = alunos.PrimeiroNome + " " + alunos.UltimoNome,
                              foto = alunos.ImageUrl,
                              horasdisciplinas =(
                                  from disciplina in _context.Disciplinas
                                  where disciplina.Id == disciplinaid
                                  select disciplina.Duracao
                             ).FirstOrDefault(),
                              horasfalta = (
                              from Falta in _context.Faltas
                              where
                                    Falta.alunoid == alunos.Id &&  Falta.disciplinaid == disciplinaid
                              select Falta.duracao).Sum()
                          }).Select(p => new FaltaAluno
                          {
                              alunoid = p.alunoid,
                              nome = p.nome,
                              foto = p.foto,
                              horasfalta = p.horasfalta,
                              percentagem = Percentagem(p.horasfalta, p.horasdisciplinas),
                              excluido = Percentagem(p.horasfalta,p.horasdisciplinas) >= 10 ? true:false

                          });


            });
            return alunos;
        }

        private static int Percentagem(int horasfaltas, int horasdisciplinas)
        {
            if (horasfaltas == 0)
            {
                return 0;
            }

            double horasdis =Convert.ToDouble(horasdisciplinas);
            double horasfalt = Convert.ToDouble(horasfaltas);

            double total = 100 /(horasdis / horasfalt);

            return Convert.ToInt16(total);
        }
    }
}
