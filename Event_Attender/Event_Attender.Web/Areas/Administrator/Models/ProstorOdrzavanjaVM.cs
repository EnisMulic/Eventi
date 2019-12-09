﻿using Event_Attender.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class ProstorOdrzavanjaVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public TipProstoraOdrzavanja TipProstoraOdrzavanja { get; set; }
        public string GradNaziv { get; set; }
    }
}