using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class FaltaDisciplinaViewModel
    {

        [Required]
        public int turmaid { get; set; }

        [Required]
        public string nometurma { get; set; }
        [Required]
        public int disciplinaid { get; set; }

        [Required]
        public int CursoId { get; set; }
        [Required]
        public string CursoNome { get; set; }

        public IEnumerable<SelectListItem> Disciplinas { get; set; }
    }
}
