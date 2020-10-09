using System.ComponentModel.DataAnnotations;

namespace Eventi.Web.ViewModels
{
    public class LoginVM
    {   
        [Required(ErrorMessage="Obavezan unos")]
        [MaxLength(40, ErrorMessage ="Maksimalna dužina je 40 karaktera")]
        public string Username { get; set; }
        
        [Required(ErrorMessage="Obavezan unos")]
        [MaxLength(30, ErrorMessage = "Maksimalna dužina je 30 karaktera")]
        public string Password { get; set; }
    }
}
