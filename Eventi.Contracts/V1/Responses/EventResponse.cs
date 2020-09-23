using Eventi.Common;
using System;

namespace Eventi.Contracts.V1.Responses
{
    public class EventResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EventCategory EventCategory { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCanceled { get; set; }
        public string Image { get; set; }
        public int OrganizerID { get; set; }
        public int? AdministratorID { get; set; }
        public int VenueID { get; set; }

    }
}
