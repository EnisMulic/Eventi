using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class Like
    {
        public int Id { get; set; }
        
        public int KorisnikId { get; set; } 
        public Korisnik Korisnik { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public DateTime DatumLajka { get; set; }

    }
}
