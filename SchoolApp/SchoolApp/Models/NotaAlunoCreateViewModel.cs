using System;

namespace SchoolApp.Models
{
    public class NotaAlunoCreateViewModel
    {
        public int alunoid { get; set; }
        public string nome { get; set; }
        public string foto { get; set; }

        public int? nota { get; set; }
        public DateTime? Data { get; set; }
        public int? novanota { get; set; }
    }
}
