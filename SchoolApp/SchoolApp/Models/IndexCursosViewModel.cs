using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class IndexCursosViewModel
    {
        public IEnumerable<Curso> Cursos { get; set; }
    }
}
