using Eventi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventi.Web.ViewModels
{
    public class RegistracijaVM
    {
        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(30)]
        public string Prezime { get; set; }

        [Required]   
       // [Phone (ErrorMessage = "Niste unijeli pravilan format broja telefona")]// problem
       [RegularExpression(@"\+[0-9]{3}[\s][0-9]{3}[\s][0-9]{3}[\s][0-9]{3}", ErrorMessage ="U formatu +387 xxx xxx xxx")]
        public string Telefon { get; set; }

        [Required]
        public int DrzavaId { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression("[A-Za-z]+", ErrorMessage ="Morate unijeti naziv nekog grada")]
        public string Grad { get; set; }  // kao string?

        [Required]
        [MaxLength(40)]
        public string Adresa { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"\d{1,10}", ErrorMessage = "Niste unijeli pravilan format")]
        public string PostanskiBroj { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Niste unijeli pravilan format")]
        public string Email { get; set; }

        [Required]
        [Remote(action:"VerifyUserName", controller:"Guest", ErrorMessage ="Ovaj username je vec u upotrebi")]  // probati
        public string Username { get; set; }  //? da li korisnik unosi ili formirati od Id korisnika pa samo prikazati korisniku

        [Required]
        [MinLength(8, ErrorMessage = "Minimalno 8 znakova")]
        [MaxLength(25)]
        public string Password { get; set; }
          
        [Required]     
       // [CreditCard(ErrorMessage ="format kreditne kartice")]//problem
       [RegularExpression(@"[0-9]{14,19}", ErrorMessage="Neispravan format kartice")]
        public string BrojKreditneKartice{ get; set; }

        public List<SelectListItem> Drzave { get; set; }
    }
}
