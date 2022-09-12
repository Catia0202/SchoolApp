using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class AlunoViewModel : Aluno
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        //[Display(Name = "Turma")]
        //[Range(1, int.MaxValue, ErrorMessage = "Tem que selecionar o aluno numa turma")]

        //public int TurmaId { get; set; }


        //public IEnumerable<SelectListItem> Turmas { get; set; }
    }
}
