using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NotaAlunoViewModel
    {

        [Required]
        public int alunoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Foto{ get; set; }

        public int CursoId{ get; set; }
        public IEnumerable<SelectListItem> Cursos { get; set; }


    }
}
