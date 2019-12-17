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
        public List<ProstorOdrzavanjaVM> Prostori { get; set; } = new List<ProstorOdrzavanjaVM>();
        public List<OrganizatorVM> Organizatori { get; set; } = new List<OrganizatorVM>();
        public List<AdministratorVM> Administratori { get; set; } = new List<AdministratorVM>();
    }
}
