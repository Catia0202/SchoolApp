using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface IAlunosRepository : IGenericRepository<Aluno>
    {
       Task<AlunoViewModel> GetAlunoByIdWithTurmaAsync(int id);

        public IQueryable GetAllWithUsers();
        public List<SelectListItem> GetListAlunos();

        Task<IQueryable<AvaliacaoAlunoDisciplinaViewModel>> GetAvaliacaoAlunoEmDisciplinaAsync(int alunoid, int turmaid);
    }
}
