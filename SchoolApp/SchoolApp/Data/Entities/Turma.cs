using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Turma :IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }


        [Display(Name ="Curso")]
        [Required]
        public int CursoId { get; set; }
        public Curso Curso { get; set; }


        [Display(Name ="Data Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data Fim")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Required]
        public DateTime DataFim { get; set; }
    }
}
