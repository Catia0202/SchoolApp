namespace SchoolApp.Data.Entities
{
    public class Turma :IEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public User User { get; set; }


       
        //public Disciplina cod_disciplina { get; set; }
    }
}
