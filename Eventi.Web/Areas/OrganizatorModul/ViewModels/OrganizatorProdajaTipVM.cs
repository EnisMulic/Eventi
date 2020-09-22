using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.ViewModels
{
    public class OrganizatorProdajaTipVM
    {
        public TipKarte _tipKarte { get; set; } 
        public string _ukupnoKarataTip { get; set; } 
        public string _brojProdatihKarataTip { get; set; } 
        public string _cijenaTip { get; set; }
        public string _postojeSjedista { get; set; }
        public int _eventID { get; set; }

    }
}
