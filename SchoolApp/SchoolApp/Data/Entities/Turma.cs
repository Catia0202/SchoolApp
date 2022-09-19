using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Turma :IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        //[Range(1, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Duracao { get; set; }

    }
}
