using System.Collections.Generic;

namespace Eventi.Web.Areas.Organizer.ViewModels
{
    public class StatistickiPodaciVM
    {

        public List<Rows> Redovi { get; set; } = new List<Rows>();

        public class Rows
        {
            public string NazivEventa { get; set; }
            public int UkupnoBrojProdatihKarata { get; set; }
            public float UkupanPrihodPoEventu { get; set; }
            public string ProsjecnaOcjenaEventa { get; set; }
        }
    }
}
