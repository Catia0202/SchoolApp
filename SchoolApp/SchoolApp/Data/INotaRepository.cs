using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Data
{
    public interface INotaRepository : IGenericRepository<Nota>
    {
        Task<IList<NotaAlunoViewModel>> GetNotasAlunoAsync();
    
        Task<IEnumerable<SelectListItem>> GetComboTurmasporAlunoAsync(int alunoid);

        Task<IEnumerable<NotaViewModel>> GetNotasAlunoDaTurma(int alunoid, int turmaid);

        Task<IQueryable<NotaAlunoCreateViewModel>> GetNotasAlunoDaTurmaDisciplina(int alunoid, int turmaid);

        Task<Nota> GetNotaByDados(int alunoid,int turmaid, int disciplinaid);


    }
}
