using System;
using System.Collections.Generic;

namespace Eventi.Web.Areas.Guest.Models
{
    public class EventSearchVM
    {
        public List<Rows> Events { get; set; }
        public class Rows
        {
            public int EventID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string VenueName { get; set; }
            public string VenueCity { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Image { get; set; }

        }
    }
}
