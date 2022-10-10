using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class FaltaTurmaViewModel
    {
        [Display(Name ="Curso")]
        [Range(1,int.MaxValue,ErrorMessage =" Selecione uma turma para continuar")]
        public int turmaid { get; set; }
        public IEnumerable<SelectListItem> Turmas { get; set; }
    }
}
