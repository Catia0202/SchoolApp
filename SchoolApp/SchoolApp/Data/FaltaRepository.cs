﻿using Microsoft.AspNetCore.Mvc.Rendering;
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

        public FaltaRepository(DataContext context, IDisciplinasRepository disciplinasRepository):base(context)
        {
            _context = context;
            _disciplinasRepository = disciplinasRepository;
        }

        public async Task<IList<FaltaAlunoViewModel>> GetFaltasAlunoAsync()
        {
            var alunos = new List<FaltaAlunoViewModel>();
            await Task.Run(() =>
            {
                alunos = _context.Aluno.Select(p => new FaltaAlunoViewModel
                {
                    alunoId = p.Id,
                    Nome = p.Nome,
                    Foto = p.ImageUrl
                }).ToList();
            });
            return alunos;
        }

        public IEnumerable<SelectListItem> GetComboTurmasporAlunoAsync(int alunoid)
        {
            var list = new List<SelectListItem>();

            var turma = _context.Aluno.Include(p => p.turma).Where(p => p.Id == alunoid).FirstOrDefault();


            list.Insert(0, new SelectListItem
            {
                Text = turma.Nome,
                Value = turma.turmaid.ToString()
            }); ;
            return list;

        }

        public async Task<IEnumerable<FaltaViewModel>> GetFaltasAlunoDaTurma(int alunoid)
        {
            var alunos = Enumerable.Empty<FaltaViewModel>();
            var disciplinas = _disciplinasRepository.GetAll().ToList();


            await Task.Run(() =>
            {
                alunos = _context.falta.Include(p => p.disciplina).Where(p => p.alunoid == alunoid).Select(p => new FaltaViewModel
                {
                    DisciplinaId = p.disciplinaid,
                    NomeDisciplina = p.disciplina.Nome,
                    Data = p.Data,
                    Duracao = p.duracao
                });
            });
            return alunos;
        }

        public async Task<IQueryable<FaltaAluno>> GetFaltasTurmaAluno(int turmaid, int disciplinaid)
        {
            var alunos = Enumerable.Empty<FaltaAluno>().AsQueryable();

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
                              horasdisciplinas =(
                                  from disciplina in _context.Disciplina
                                  where disciplina.Id == disciplinaid
                                  select disciplina.Duracao
                             ).FirstOrDefault(),
                              horasfalta = (
                              from Falta in _context.falta
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