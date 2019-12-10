using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class OrganizatorDisplayVM
    {
        public List<OrganizatorVM> Organizatori { get; set; } = new List<OrganizatorVM>();
        public OrganizatorVM OnDisplay { get; set; } = new OrganizatorVM();
    }
}
