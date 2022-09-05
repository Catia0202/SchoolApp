using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;

namespace SchoolApp.Data
{
    public class TurmasRepository : GenericRepository<Turma>, ITurmasRepository
    {
        private readonly DataContext _context;

        public TurmasRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
