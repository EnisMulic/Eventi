﻿using Event_Attender.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.Areas.Administrator.Models
{
    public class IzvodjacVM
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }
        [Required]
        public TipIzvodjaca TipIzvodjaca { get; set; }
    }
}
