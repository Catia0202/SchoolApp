using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SchoolApp.Models
{
    public class AddTurmaViewModel
    {
        [Display(Name = "Turma")]
        [Range(1, int.MaxValue, ErrorMessage = "Tem que inserir o aluno numa turma")]

        public int TurmaId { get; set; }


        public IEnumerable<SelectListItem> Turmas { get; set; }
    }
}
