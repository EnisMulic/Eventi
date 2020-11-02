using System;
using System.Collections.Generic;

namespace Eventi.Web.Areas.Client.Models
{
    public class VisitedEventsVM
    {
        public int ClientID { get; set; }
        public List<Row> Events { get; set; }
        public class Row
        {
            public float SumPayed { get; set; }
            public int PurchaseID { get; set; }
            public int EventID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string VenueCity { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Image { get; set; }
        }
        
    }
}
