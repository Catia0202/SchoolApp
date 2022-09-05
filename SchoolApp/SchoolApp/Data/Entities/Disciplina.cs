namespace SchoolApp.Data.Entities
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Descrição { get; set; }

        public Turma cod_turma { get; set; }
    }
}
