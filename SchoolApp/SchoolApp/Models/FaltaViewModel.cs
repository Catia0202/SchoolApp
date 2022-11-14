using System;

namespace SchoolApp.Models
{
    public class FaltaViewModel
    {
        public int DisciplinaId { get; set; }

        public string NomeDisciplina { get; set; }

        public DateTime Data { get; set; }


        public int alunoid { get; set; }
        public string nome { get; set; }
        public string foto { get; set; }
        public int Duracao { get; set; }
        public int percentagem { get; set; }
        public bool excluido { get; set; }
        public int horasdisciplina { get; set; }

        public int horasfalta { get; set; }
    }
}
