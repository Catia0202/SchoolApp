using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;
using System.Linq;

namespace SchoolApp.Helpers
{
    public interface IConverterHelper
    {
        Aluno ToAluno(AlunoViewModel model, string path, bool isNew);

        AlunoViewModel ToAlunoViewModel(Aluno aluno, User user);

        IQueryable<AlunoViewModel> AlunosToAlunoViewModels(IQueryable<Aluno> alunos);
    }
}
