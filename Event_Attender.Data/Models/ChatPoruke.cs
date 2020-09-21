using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class ChatPoruke
    {
        public int Id { get; set; }
        public string Poruka { get; set; }
        public DateTime Kreirana { get; set; }
        public int OsobaId { get; set; }
        public Osoba Osoba { get; set; }
    }
}
