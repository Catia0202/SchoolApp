﻿using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class TodasNotasDoAlunoViewModel
    {

        public string Nome { get; set; }
        
        public string Turma { get; set; }

        public string foto { get; set; }

        public string nomedisciplina { get; set; }
        public IEnumerable<NotaViewModel> Notas { get; set; }
        public bool aprovado { get; set; }
    }
}
