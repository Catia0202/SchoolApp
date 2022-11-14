using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class AvaliacaoAlunoDisciplinaViewModel
    {
        public int Iddisciplina { get; set; }
        [Display(Name = "Disciplina")]
        public string Nomedisciplina { get; set; }
        [Display(Name = "Horas da Disciplina")]
        public int Duracaodisciplina { get; set; }


        public int? Nota { get; set; }
        [Display(Name = "Data ")]
        public DateTime? DataNota { get; set; }

        public bool Chumbounota { get; set; }
        [Display(Name = "Horas Faltadas")]
        public int Horasfalta { get; set; }
        [Display(Name = "Percentagem de Faltas")]
        public int Percentagemfalta { get; set; }
        public bool ChumbouFaltas { get; set; }

    }
}
