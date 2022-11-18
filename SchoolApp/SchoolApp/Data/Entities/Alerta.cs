using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Alerta : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Problema { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string Estado { get; set; }

    }
}
