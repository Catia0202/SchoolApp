using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;


namespace SchoolApp.Data
{
    public interface IDisciplinasRepository : IGenericRepository<Disciplina>
    {
        //IEnumerable<SelectListItem> GetListDisciplinasNaTurma(int turmaId);
    }
}
