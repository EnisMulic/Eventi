﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttender.Data.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        public string PostanskiBroj { get; set; }

        public int OsobaId { get; set; }  
        public Osoba Osoba { get; set; }
    
    }
}
