using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttender.Data.Models
{
    public class Administrator
    {
        public int Id { get; set; }
       
        public int OsobaId { get; set; }
        public Osoba Osoba { get; set; } 
       
    }
}
