using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Attender.Data.Models
{
    public class IzvodjacEvent
    {
        public int Id { get; set; }

        public int IzvodjacId { get; set; }
        public Izvodjac Izvodjac { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
