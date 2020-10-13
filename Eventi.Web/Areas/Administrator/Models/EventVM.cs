using Eventi.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class EventVM
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [DisplayName("Start Date")]
        public DateTime Start { get; set; }

        [Required]
        [DisplayName("End Date")]
        public DateTime End { get; set; }

        [Required]
        public EventCategory EventCategory { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCanceled { get; set; }

        public string Image { get; set; }

        [Required]
        public int OrganizerID { get; set; }

        [DisplayName("Organizer Name")]
        public string OrganizerName { get; set; }

        [Required]
        public int? AdministratorID { get; set; }

        [DisplayName("Administrator Name")]
        public string AdministratorName { get; set; }

        [Required]
        public int VenueID { get; set; }

        [DisplayName("Prostor održavanja")]
        public string VenueName { get; set; }

        public List<SelectListItem> Organizers { get; set; }
        public List<SelectListItem> Administrators { get; set; }
        public List<SelectListItem> Venues { get; set; }
    }
}
