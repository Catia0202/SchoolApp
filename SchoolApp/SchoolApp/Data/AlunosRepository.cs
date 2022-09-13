using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using SchoolApp.Data.Entities;
using SchoolApp.Helpers;
using SchoolApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public class AlunosRepository : GenericRepository<Aluno> , IAlunosRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AlunosRepository(DataContext context,IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        //public async Task<IQueryable<Turma>> GetTurmasAsync(string username)
        //{
        //  var user =await _userHelper.GetUserByEmailAsync(username);
        //    if(user== null)
        //    {
        //        return null;
        //    }

        //        return _context.Aluno.Include(o => o.turma).OrderBy(o => o.turma.Nome);


        //}
    }
}
