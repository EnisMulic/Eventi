using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.Areas.Administrator.Models
{
    public class ChatVM
    {
        public int AdministratorId { get; set; } //OsobaId
        public string Username { get; set; }

        public List<Row> Rows { get; set; }
        public class Row
        {
            public int ChatPorukaId { get; set; }
            public string Poruka { get; set; }
            public int AutorId { get; set; }
            public string Autor { get; set; }
            public string Kreirana { get; set; }
        }
    }
}
