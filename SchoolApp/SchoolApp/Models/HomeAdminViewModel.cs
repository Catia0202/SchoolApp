using SchoolApp.Data.Entities;
using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class HomeAdminViewModel
    {
       
        public int TotalAlunos { get; set; }
        public int TotalTurmas { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalCursos { get; set; }

        public IEnumerable<UsersComRoles> Users { get; set; }
    }
}
