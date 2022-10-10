using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Aluno> Aluno { get; set; }

        public DbSet<Disciplina> Disciplina { get; set; }

        public DbSet<Falta> falta { get; set; }

        public DbSet<Nota> Nota { get; set; }
         
        public DbSet<Turma> turma { get; set; }

        public DbSet<TurmaDisciplina> turmaDisciplina { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
