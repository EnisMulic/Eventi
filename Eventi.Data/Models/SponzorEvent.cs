using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{   public enum Prioritet { Gold, Silver, Bronze}
    public class SponzorEvent
    {
        public int Id { get; set; }
        
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int SponzorId { get; set; }
        public Sponzor Sponzor { get; set; }

        public Prioritet Prioritet { get; set; }

    }
}
