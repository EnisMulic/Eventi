using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public enum TipProstoraOdrzavanja { Sala, Dvorana, Stadion}
    public class ProstorOdrzavanja
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public TipProstoraOdrzavanja TipProstoraOdrzavanja { get; set; }

        public int GradId { get; set; }
        public Grad Grad { get; set; }
    }
}
