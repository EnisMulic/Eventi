using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.ViewModels
{
    public class EventInfo
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public string VrijemeOdrzavanja { get; set; }
        public Kategorija Kategorija { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsOtkazan { get; set; }
        public string OrganizatorNaziv { get; set; }
        public string ProstorOdrzavanjaNaziv { get; set; }
    }
}
