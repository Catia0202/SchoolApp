namespace SchoolApp.Data.Entities
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Disciplina cod_disciplina { get; set; }
    }
}
