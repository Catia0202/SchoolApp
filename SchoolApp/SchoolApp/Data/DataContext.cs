using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.Entities;
using System.Linq;

namespace MyLeasing.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<Falta> Faltas { get; set; }

        public DbSet<Nota> Notas { get; set; }
         
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Configuracao> Configuracoes{ get; set; }

        public DbSet<CursoDisciplina> CursoDisciplinas { get; set; }

        public DbSet<Curso> Cursos { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
