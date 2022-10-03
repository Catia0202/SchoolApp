using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class NotaViewModel : Nota
    {
        public int TurmaId { get; set; }


        public IEnumerable<SelectListItem> Turmas { get; set; }

        public int DisciplinaID { get; set; }


        public IEnumerable<SelectListItem> Disciplinas { get; set; }

    }
}
