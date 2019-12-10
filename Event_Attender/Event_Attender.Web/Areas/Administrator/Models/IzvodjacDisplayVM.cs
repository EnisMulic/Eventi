using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class IzvodjacDisplayVM
    {
        public List<IzvodjacVM> Izvodjaci { get; set; } = new List<IzvodjacVM>();
        public IzvodjacVM OnDisplay { get; set; } = new IzvodjacVM();
    }
}
