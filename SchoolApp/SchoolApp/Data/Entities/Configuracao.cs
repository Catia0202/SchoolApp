using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Configuracao : IEntity
    {

        public int Id { get; set; }

        public int PercentagemdeFaltas { get; set; }

        public int MaximoDisciplinasPorTurma { get; set; }
        [Display(Name = "Nº Máximo de Alunos na Turma")]
        [Required(ErrorMessage = "{0} é necessário")]
        [Range(0, 100, ErrorMessage = "Insira um valor entre {1} e {2}.")]
        public int MaximoAlunosNaTurma { get; set; }


    }
}
