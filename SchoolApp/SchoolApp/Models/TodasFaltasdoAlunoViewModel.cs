using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class TodasFaltasdoAlunoViewModel
    {
        public string Nome { get; set; }

        public string Turma { get; set; }

        public string foto { get; set; }

        public IEnumerable<FaltaViewModel> Faltas { get; set; }
    }
}
