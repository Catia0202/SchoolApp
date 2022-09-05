using Microsoft.AspNetCore.Http;
using SchoolApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class AlunoViewModel : Aluno
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
