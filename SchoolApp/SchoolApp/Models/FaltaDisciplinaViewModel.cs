using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class FaltaDisciplinaViewModel
    {

       
        [Display(Name = "Curso")]
        [Range(1, int.MaxValue, ErrorMessage = " Selecione uma turma para continuar")]
        public int turmaid { get; set; }

        [Required]
        public string nometurma { get; set; }

        [Display(Name = "Disciplina")]
        [Range(1, int.MaxValue, ErrorMessage = " Selecione uma Disciplina para continuar")]
        public int disciplinaid { get; set; }

        [Required]
        public int CursoId { get; set; }
        [Required]
        public string CursoNome { get; set; }

        public IEnumerable<SelectListItem> Disciplinas { get; set; }
    }
}
