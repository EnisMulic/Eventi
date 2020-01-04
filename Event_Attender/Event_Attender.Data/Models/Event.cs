using System;
using System.Collections.Generic;
using System.Text;

public enum Kategorija {Muzika,Kultura,Sport}

namespace Event_Attender.Data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public string VrijemeOdrzavanja { get; set; } 
        public Kategorija Kategorija { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsOtkazan { get; set; }
        
        public string Slika { get; set; }

        public int OrganizatorId { get; set; }
        public Organizator Organizator { get; set; }

        public int? AdministratorId { get; set; }
        public Administrator Administrator { get; set; }

        public int ProstorOdrzavanjaId { get; set; }
        public ProstorOdrzavanja ProstorOdrzavanja { get; set; }
    }
}
