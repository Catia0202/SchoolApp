using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Data
{
    public interface ITurmasRepository : IGenericRepository<Turma>
    {
        IEnumerable<SelectListItem> GetComboTurmas();
    }
}
