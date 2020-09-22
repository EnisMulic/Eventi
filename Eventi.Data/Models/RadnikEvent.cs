using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class RadnikEvent  //zaduzenje
    {
        public int Id { get; set; }

        public int RadnikId { get; set; }
        public Radnik Radnik { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
