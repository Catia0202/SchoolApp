using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;


namespace SchoolApp.Data
{
    public interface IDisciplinasRepository : IGenericRepository<Disciplina>
    {
      public List<SelectListItem> GetListDisciplinas();


    }
}
