using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Data.Entities
{
    public class Aluno : IEntity
    {
        public int Id { get; set; }
        [Display(Name ="Primeiro Nome")]
        public string PrimeiroNome { get; set; }
        [Display(Name = "Ultimo Nome")]
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public int Telemovel { get; set; }

        public string Data_Nascimento { get; set; }

        public string Morada { get; set; }

        public string Genero { get; set; }
        public User User { get; set; }
        //[ForeignKey("turma")]
        public Turma turma { get; set; }

        public int turmaid { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public Guid ImageId { get; set; }

        //public IEnumerable<Turma> Turmas { get; set; }
        // public string ImageFullPath => ImageId == Guid.Empty ? $"https://localhost:44318/images/noimage.png" :
        //$"https://myleasingstoragesnt.blob.core.windows.net/lessees/{ImageId}";

    
    }
}
