﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NotaDisciplinaViewModel
    {
        [Required]
        public int Turmaid { get; set; }

        [Required]
        public string Nometurma { get; set; }
        [Required]
        public int CursoId { get; set; }
        [Required]
        public string CursonNome { get; set; } 
        [Required]
        public int Disciplinaid { get; set; }

        public IList<NotaAlunoCreateViewModel> Alunos { get; set; }
        public IEnumerable<SelectListItem> Disciplinas { get; set; }
    }
}
