using SchoolApp.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface IAlunosRepository : IGenericRepository<Aluno>
    {
        //Task<IQueryable<Turma>> GetTurmasAsync(string username);
    }
}
