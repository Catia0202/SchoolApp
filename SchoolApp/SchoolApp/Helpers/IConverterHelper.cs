using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System;

namespace SchoolApp.Helpers
{
    public interface IConverterHelper
    {
        Aluno ToAluno(AlunoViewModel model, string path, bool isNew);

        AlunoViewModel ToAlunoViewModel(Aluno owner);

      
    }
}
