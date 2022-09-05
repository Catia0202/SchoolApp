using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;

namespace SchoolApp.Data
{
    public class AlunosRepository : GenericRepository<Aluno> , IAlunosRepository
    {
        private readonly DataContext _context;

        public AlunosRepository(DataContext context) : base(context)
        {
            _context = context;
        }




    }
}
