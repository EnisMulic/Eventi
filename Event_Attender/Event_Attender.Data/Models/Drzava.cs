using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventAttender.Data.Models
{
    public class Drzava
    {   [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
    }
}
