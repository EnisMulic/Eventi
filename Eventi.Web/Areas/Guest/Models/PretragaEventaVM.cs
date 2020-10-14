﻿using Eventi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Guest.Models
{
    public class PretragaEventaVM
    {
        public List<Rows> Eventi { get; set; }
        public class Rows
        {
            public int EventId { get; set; }
            public string Naziv { get; set; }
            public string Kategorija { get; set; }
            public string ProstorOdrzavanjaNaziv { get; set; }
            public string ProstorOdrzavanjaGrad { get; set; }
            public string DatumOdrzavanja { get; set; }
            public string VrijemeOdrzavanja { get; set; }
            public string Slika { get; set; }

        }
    }
}
