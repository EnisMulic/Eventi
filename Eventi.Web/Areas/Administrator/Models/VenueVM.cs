using Eventi.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class VenueVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public VenueCategory VenueCategory { get; set; }
        [Required]
        public int CityID { get; set; }
        public string CityName { get; set; }

        public List<SelectListItem> Cities { get; set; }
    }
}
