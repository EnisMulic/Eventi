using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class Organizator
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }

        public int? GradId { get; set; }  
        public Grad Grad { get; set; }

        public int? LogPodaciId { get; set; } 
        public LogPodaci LogPodaci { get; set; }
    }
}
