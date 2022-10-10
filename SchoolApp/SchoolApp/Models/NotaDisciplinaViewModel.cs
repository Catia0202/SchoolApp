using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NotaDisciplinaViewModel
    {
        [Required]
        public int turmaid { get; set; }

        [Required]
        public string nometurma { get; set; }
        [Required]
        public int disciplinaid { get; set; }


   
        public IEnumerable<SelectListItem> Disciplinas { get; set; }
    }
}
