using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class EventDisplayVM
    {
        public List<EventVM> Events { get; set; } = new List<EventVM>();
        public EventVM OnDisplay { get; set; } = new EventVM();
    }
}
