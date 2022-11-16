using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Curso : IEntity
    {
        public int Id { get; set; }
        [MaxLength(70)]
        [Required]
        public string Nome { get; set; }
        [Required]
        [Range(1,1000)]
        public int Duracao { get; set; }
        public string Descricao { get; set; }
        public string Fotourl { get; set; }
    }
}
