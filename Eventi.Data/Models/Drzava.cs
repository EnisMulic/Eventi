using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventi.Data.Models
{
    public class Drzava
    {   [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
