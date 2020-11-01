using System;
using System.Collections.Generic;

namespace Eventi.Web.Areas.Client.Models
{
    public class PretragaEventaVM
    {
        public List<Rows> Events { get; set; }
         public int ClientID { get; set; }
        
        public class Rows
        {
            public int EventID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string VenueName { get; set; }
            public string VenueAddress { get; set; }
            public string VenueCity { get; set; }
            public DateTime Start { get; set; }  
            public DateTime End { get; set; }  
            public string Image { get; set; }
        }         
    }
}
