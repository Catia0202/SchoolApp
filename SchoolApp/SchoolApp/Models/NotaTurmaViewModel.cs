using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NotaTurmaViewModel
    {
        [Display(Name = "Cursos")]
        [Required(ErrorMessage="{0} é necessário")]
        [Range(1, int.MaxValue, ErrorMessage ="Selecione um curso para continuar")]
        public int turmaid { get; set; }

        public IEnumerable<SelectListItem> Turmas { get; set; }
    }
}
