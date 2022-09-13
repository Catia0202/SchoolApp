using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;

namespace SchoolApp.Helpers
{
    public class ConvertHelper : IConverterHelper 
    {
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
                turmaid = model.turmaid
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
                turmaid= aluno.turmaid
            };
        }

    }
}
