using System;


namespace SchoolApp.Data.Entities
{
    public class Falta :IEntity
    {
        public int Id { get; set; }
        public DateTime Data{ get; set; }

        

        public int alunoid { get; set; }    
        public Aluno aluno { get; set; }


        public int disciplinaid { get; set; }
        public Disciplina disciplina { get; set; }

        public int duracao { get; set; }

    }
}
