using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Data.Models
{
    public class Sjediste
    {
        public int Id { get; set; }
        public int BrojSjedista { get; set; } // =ProdajaTip.brojProdatihKarata

        public int KartaId { get; set; }
        public Karta Karta { get; set; }
    }
}
