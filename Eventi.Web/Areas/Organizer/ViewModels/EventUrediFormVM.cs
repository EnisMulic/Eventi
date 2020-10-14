using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Eventi.Web.Areas.Organizer.ViewModels
{

    public class EventUrediFormVM
    {
        private Array vrijednostiKategorije = Enum.GetValues(typeof(Kategorija));

        public int EventId { get; set; }
        public string NazivEventaOd { get; set; }
        public string OpisEventaOd { get; set; }
        public string VrijemeOdrzavanjaOd { get; set; }
      
        public int KategorijaOdabir { get; set; }
        public List<SelectListItem> Kategorije { get; set; } = new List<SelectListItem>();

        public int SponzorOdabir { get; set; }
        public List<SelectListItem> Sponzori { get; set; } = new List<SelectListItem>();

        public EventUrediFormVM()
        {
            foreach(var item in vrijednostiKategorije)
            {
                Kategorije.Add(new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = Enum.GetName(typeof(Kategorija), item)
                });
            }
        }
      
    }
}
