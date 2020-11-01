using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Client.Models
{
    public class EventClientVM
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
        public int ClientID { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientCity { get; set; }
        public string ClientCreditCardNumber { get; set; }
    }
}
