using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.Data.Entities;
using System;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class NotaViewModel 
    {


        public int DisciplinaId { get; set; }

        public string NomeDisciplina { get; set; }

        public DateTime Data { get; set; }


        public int Nota { get; set; }
        

     
    }
}
