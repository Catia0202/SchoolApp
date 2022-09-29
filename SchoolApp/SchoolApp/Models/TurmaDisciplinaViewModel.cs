using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;


namespace SchoolApp.Models
{

    public class TurmaDisciplinaViewModel : TurmaDisciplina
    {
        public List<SelectListItem> listdisciplinas { get; set; }

        public string disciplinax;

        public string turmas;
        public int[] disciplinaids { get; set; }

    }
}
