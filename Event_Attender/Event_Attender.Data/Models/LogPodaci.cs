using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttender.Data.Models
{
    public class LogPodaci
    {
        public int Id { get; set; }
        public string Email { get; set; }  
       
        public string Username { get; set; }  //? da li korisnik unosi ili formirati od Id korisnika pa samo prikazati korisniku
        public string Password { get; set; }

    }
}
