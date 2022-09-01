namespace SchoolApp.Data.Entities
{
    public class falta
    {
        public int Id { get; set; }
        public string Dia { get; set; }

        public string Hora { get; set; }

        public Aluno cod_aluno { get; set; }
        public Disciplina cod_disciplina { get; set; }
    }
}
