using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class Radnik
    {
        public int Id { get; set; }
        
        public int OsobaId { get; set; }  // var3
        public Osoba Osoba { get; set; } 
    }
}
