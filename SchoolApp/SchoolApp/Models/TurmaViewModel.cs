using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class TurmaViewModel : Turma
    {


        public IEnumerable<SelectListItem> Cursos { get; set; }
    }
}
