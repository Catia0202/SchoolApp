using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class IndexTurmasDisciplinasViewModel
    {
        public IEnumerable<Disciplina> DisciplinasDaTurma{ get; set; }

        public Turma Turma { get;set; }
        public IEnumerable<Turma> Turmas { get; set; }

    }
}
