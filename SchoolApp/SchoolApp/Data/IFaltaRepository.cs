using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface IFaltaRepository : IGenericRepository<Falta>
    {
        Task<IList<FaltaAlunoViewModel>> GetFaltasAlunoAsync();
        IEnumerable<SelectListItem> GetComboTurmasporAlunoAsync(int alunoid);

        Task<IEnumerable<FaltaViewModel>> GetFaltasAlunoDaTurma(int alunoid);

        Task<IQueryable<FaltaAluno>> GetFaltasTurmaAluno(int turmaid, int disciplinaid);
    }
}
