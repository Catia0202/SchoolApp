using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Data.Entities
{
    public class Aluno :IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
        public int Telemovel { get; set; }

        public string Data_Nascimento { get; set; }

        public string Morada { get; set; }

        public string Genero { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }


        // public string ImageFullPath => ImageId == Guid.Empty ? $"https://localhost:44318/images/noimage.png" :
        //$"https://myleasingstoragesnt.blob.core.windows.net/lessees/{ImageId}";

        public User User { get; set; }
        //public Turma cod_turma { get; set; }
    }
}
