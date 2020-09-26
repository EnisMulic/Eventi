using Eventi.Common;
using System;

namespace Eventi.Contracts.V1.Requests
{
    public class EventSearchRequest
    {
        public string Name { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public EventCategory? EventCategory { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsCanceled { get; set; }
        public int? VenueID { get; set; }
    }
}
