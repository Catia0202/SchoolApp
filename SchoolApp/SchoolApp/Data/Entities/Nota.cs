﻿using System;
using System.ComponentModel.DataAnnotations;


namespace SchoolApp.Data.Entities
{
    public class Nota : IEntity
    {
        public int Id { get; set; }
        public int NotaAluno { get; set; }

        public string Anotação { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data { get; set; }
        public Aluno aluno { get; set; }
        public int idaluno { get; set; }
        public Turma turma { get; set; }
        public int idturma { get; set; }
        public Disciplina disciplina { get; set; }
        public int iddisciplina { get; set; }
    }
}
