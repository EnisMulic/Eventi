using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Attender.Web.ViewModels
{
    public class LoginVM
    {   [Required(ErrorMessage ="Obavezan unos")]
        [MaxLength(40)]
        public string username { get; set; }
        [Required(ErrorMessage ="Obavezan unos")]
        [MaxLength(25)]
        public string password { get; set; }
    }
}
