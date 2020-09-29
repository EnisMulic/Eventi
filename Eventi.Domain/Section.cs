using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Domain
{
    public class Section
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int VenueID { get; set; }
        public Venue Venue { get; set; }
    }
}
