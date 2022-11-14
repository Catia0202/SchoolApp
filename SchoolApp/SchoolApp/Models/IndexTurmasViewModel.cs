using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class IndexTurmasViewModel
    {
        public IEnumerable<Turma> Turmas { get; set; }
    }
}
