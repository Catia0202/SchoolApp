namespace SchoolApp.Data.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public string NotaAluno { get; set; }

        public string Anotação { get; set; }

        public Aluno cod_aluno { get; set; }
        public Disciplina cod_disciplina { get; set; }

    }
}
