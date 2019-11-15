using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttender.Data.Models
{   public enum TipIzvodjaca {Pjevac, Bend, Grupa, Hor, FudbalskiTim}
    public class Izvodjac
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public TipIzvodjaca TipIzvodjaca { get; set; }
    }
}
