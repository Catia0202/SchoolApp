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

        public async Task<AlunoViewModel> GetAlunoByIdWithTurmaAsync(int id)
        {
            var aluno = await _context.Aluno.Include(x => x.turma).Where(x => x.Id == id).FirstOrDefaultAsync();
            if (aluno == null)
            {
                return null;
            }
            return new AlunoViewModel
            {
                Id = aluno.Id,
                ImageUrl = aluno.ImageUrl,
                Nome = aluno.Nome,
                Data_Nascimento = aluno.Data_Nascimento,
                Email = aluno.Email,
                Genero = aluno.Genero,
                Morada = aluno.Morada,
                Telemovel = aluno.Telemovel,
                User = aluno.User,
                turmaid = aluno.turmaid,
                turma = aluno.turma
                //Turmas = _turmasRepository.GetComboTurmas()
            };
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
