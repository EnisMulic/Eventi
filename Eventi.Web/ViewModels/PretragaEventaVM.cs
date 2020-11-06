using System.Collections.Generic;

namespace Eventi.Web.ViewModels
{
    public class PretragaEventaVM
    {
        //public List<Event> eventi { get; set; }
        public List<Rows> Eventi { get; set; }
        public class Rows
        {
            public int EventId { get; set; }
            public string Naziv { get; set; }
            public string Kategorija { get; set; }
            //public string ProstorOdrzavanjaNaziv { get; set; }
            //public string ProstorOdrzavanjaGrad { get; set; }
            //public string DatumOdrzavanja { get; set; }
            //public string VrijemeOdrzavanja { get; set; }
            //public byte[] Slika { get; set; }
            public string Slika { get; set; }
            public bool SoldOut { get; set; }
        }
    }
}
