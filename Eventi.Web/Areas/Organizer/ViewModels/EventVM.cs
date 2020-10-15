using Eventi.Common;
using System;

namespace Eventi.Web.Areas.Organizer.ViewModels
{
    public class EventVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EventCategory EventCategory { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCanceled { get; set; }
        public string OrganizerName { get; set; }
        public int OrganizerID { get; set; }
        public int VenueID { get; set; }
        public string VenueName { get; set; }
    }
}
