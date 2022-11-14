using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class AlunoAvaliacoesViewModel
    {
        public string NomeCurso { get; set; }

        public IEnumerable<AvaliacaoAlunoDisciplinaViewModel> AvaliacaoDisciplinas { get; set; }
        
    }
}
