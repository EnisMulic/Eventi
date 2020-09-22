using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class Kupovina
    {
        public int Id { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }


    }
}
