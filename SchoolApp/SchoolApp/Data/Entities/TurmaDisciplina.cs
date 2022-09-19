using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class TurmaDisciplina : IEntity 
    {
        public int Id { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "{0} is required")]
        public int TurmaId { get; set; }
        public Turma turma { get; set; }

        [Display(Name = "Disciplina")]
        [Required(ErrorMessage = "{0} is required")]
        public int DisciplinaId { get; set; }

        public Disciplina Disciplina { get; set; }

    }
}
