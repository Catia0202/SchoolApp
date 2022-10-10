using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class FaltaAlunoTurmaDisciplinaViewModel
    {
        [Required]
        public int turmaid{ get; set; }

        [Required]
        public string nometurma { get; set; }

       
        [Required]
        public int disciplinaid { get; set; }

        public string nomedisciplina { get; set; }

        [Required]
        public int duracao { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime Data { get; set; }

        public IList<FaltaAluno> FaltaAlunos { get; set; }
    }
}
