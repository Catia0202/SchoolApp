using SchoolApp.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;
using System.Linq;

namespace SchoolApp.Helpers
{
    public class ConvertHelper : IConverterHelper 
    {
        private readonly ITurmasRepository _turmasRepository;

        public ConvertHelper(ITurmasRepository turmasRepository)
        {
            _turmasRepository = turmasRepository;
        }

        public IQueryable<AlunoViewModel> AlunosToAlunoViewModels(IQueryable<Aluno> alunos)
        {
            return alunos.Select(aluno => new AlunoViewModel
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
            });
        }

        public  Aluno ToAluno(AlunoViewModel model, string path, bool isNew)
        {
            return new Aluno
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Nome = model.Nome,
                Data_Nascimento =model.Data_Nascimento,
                Email = model.Email,
                Genero =model.Genero,
                Morada = model.Morada,
                Telemovel = model.Telemovel,
                User = model.User,
                turmaid = model.turmaid,
                turma = model.turma
            };
        }

        public AlunoViewModel ToAlunoViewModel(Aluno aluno)
        {
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
                turma = aluno.turma,
                Turmas = _turmasRepository.GetComboTurmas()
            };
        }

    }
}
