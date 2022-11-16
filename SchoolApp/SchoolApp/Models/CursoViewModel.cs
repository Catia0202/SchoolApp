using Microsoft.AspNetCore.Http;
using SchoolApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class CursoViewModel : Curso
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
