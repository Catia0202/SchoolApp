using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NotaAlunoTurmaDisciplinaViewModel
    {

        [Required]
        public int disciplinaId { get; set; }


        public string disciplinaNome { get; set; }
        [Required]
        public int Cursoid { get; set; }
        public int Duracao { get; set; }
        [Required]
        public int TurmaId { get; set; }

        public string turmaNome { get; set; }
        [Required]
        public string CursoNome { get; set; }

        public IList<NotaAlunoCreateViewModel> Alunos { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime Data { get; set; }

     
    }
}
