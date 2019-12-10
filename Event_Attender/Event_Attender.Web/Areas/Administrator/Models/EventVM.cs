using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class EventVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [DisplayName("Datum održavanja")]
        public DateTime DatumOdrzavanja { get; set; }
        [DisplayName("Vrijeme održavanja")]
        public string VrijemeOdrzavanja { get; set; }
        public Kategorija Kategorija { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsOtkazan { get; set; }
        public byte[] Slika { get; set; }

        [DisplayName("Naziv organizatora")]
        public string OrganizatorNaziv { get; set; }

        [DisplayName("Naziv administratora")]
        public string AdministratorNaziv { get; set; }

        [DisplayName("Prostor održavanja")]
        public string ProstorOdrzavanjaNaziv { get; set; }
    }
}
