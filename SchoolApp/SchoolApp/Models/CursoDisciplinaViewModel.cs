using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;


namespace SchoolApp.Models
{

    public class CursoDisciplinaViewModel : CursoDisciplina
    {
        public List<SelectListItem> listdisciplinas { get; set; }

        public string disciplinax;

        public int[] disciplinaids { get; set; }

    }
}
