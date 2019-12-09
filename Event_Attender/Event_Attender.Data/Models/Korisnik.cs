using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string PostanskiBroj { get; set; }

        public string BrojKreditneKartice { get; set; }  // ?
        public int OsobaId { get; set; }  
        public Osoba Osoba { get; set; }


    
    }
}
