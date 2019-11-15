using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttender.Data.Models
{
    public class Karta
    {
        public int Id { get; set; }
        public TipKarte Tip{get;set;} // iz KupovinaTip   (=)
        public float Cijena { get; set; } //iz ProdajaTip.CijenaTip  (=)

        public int KupovinaTipId { get; set; }
        public KupovinaTip KupovinaTip { get; set; }


    }
}
