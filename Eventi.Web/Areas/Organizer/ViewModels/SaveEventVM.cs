using System;

namespace Eventi.Web.Areas.Organizer.ViewModels
{
    public class SaveEventVM
    {
        public int OrganizerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string OptRadio { get; set; }
        public string OptCombo { get; set; }
    }
}
