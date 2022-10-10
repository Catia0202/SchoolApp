﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class FaltaAlunoViewModel
    {

        [Required]
        public int alunoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Foto { get; set; }

        public int TurmaId { get; set; }
        public IEnumerable<SelectListItem> Turmas { get; set; }
    }
}
