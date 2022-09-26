﻿using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System.Collections.Generic;


namespace SchoolApp.Models
{

    public class TurmaDisciplinaViewModel : TurmaDisciplina
    {
        public List<SelectListItem> listdisciplinas { get; set; }

        public int[] disciplinaids { get; set; }

    }
}