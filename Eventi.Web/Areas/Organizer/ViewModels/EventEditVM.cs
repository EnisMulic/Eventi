using Eventi.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;


namespace Eventi.Web.Areas.Organizer.ViewModels
{

    public class EventEditVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
      
        public int EventCategorySelected { get; set; }
        public List<SelectListItem> EventCategory { get; set; } = new List<SelectListItem>();

        public int SponsorSelected { get; set; }
        public List<SelectListItem> Sponsors { get; set; } = new List<SelectListItem>();

        public EventEditVM()
        {
            Array categories = Enum.GetValues(typeof(EventCategory));
            foreach (var item in categories)
            {
                EventCategory.Add(new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = Enum.GetName(typeof(EventCategory), item)
                });
            }
        }
      
    }
}
