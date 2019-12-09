using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class Radnik
    {
        public int Id { get; set; }
        
        public int OsobaId { get; set; }  // var3
        public Osoba Osoba { get; set; } 
    }
}
