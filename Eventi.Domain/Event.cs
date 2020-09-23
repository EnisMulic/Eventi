using Eventi.Common;
using System;

namespace Eventi.Domain
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EventCategory EventCategory { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCanceled { get; set; }
        public string Image { get; set; }
        public int OrganizerID { get; set; }
        public Organizer Organizer { get; set; }
        public int? AdministratorID { get; set; }
        public Administrator Administrator { get; set; }
        public int VenueID { get; set; }
        public Venue Venue { get; set; }
    }
}
