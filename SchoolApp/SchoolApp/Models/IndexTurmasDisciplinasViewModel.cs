using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class IndexTurmasDisciplinasViewModel
    {
        public IEnumerable<Disciplina> DisciplinasDaTurma{ get; set; }

        public Curso Curso { get;set; }
        public IEnumerable<Curso> Cursos { get; set; }

        public IEnumerable<Turma> Turmas { get; set; }
    }
}
