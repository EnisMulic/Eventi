using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.ModulKorisnik.Models
{
    public class RecenzijaVM
    {
        public int page { get; set; }
        public int RecenzijaId { get; set; }
        public int KupovinaId { get; set; }
        public string NazivEventa { get; set; }
        [MaxLength(1000, ErrorMessage ="Preskocili ste maksimalan broj karaktera")]
        public string Komentar { get; set; }
        public int Ocjena { get; set; }
    }
}
