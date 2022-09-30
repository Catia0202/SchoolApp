using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public int NotaAluno { get; set; }

        public string Anotação { get; set; }

        public int id_aluno { get; set; }
        public Aluno aluno { get; set; }

        public int id_disciplina { get; set; }
        public Disciplina disciplina { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data { get; set; }
    }
}
