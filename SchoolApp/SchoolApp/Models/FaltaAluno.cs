namespace SchoolApp.Models
{
    public class FaltaAluno
    {
        public int alunoid { get; set; }
        public string nome { get; set; }
        public string foto { get; set; }
        public int percentagem { get; set; }

        public int duracao { get; set; }

        public bool excluido { get; set; }

        public int horasdisciplina { get; set; }

        public int horasfalta { get; set; } 
    }
}
