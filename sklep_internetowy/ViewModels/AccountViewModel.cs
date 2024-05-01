using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace sklep_internetowy.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Musisz wprowadzić e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Musisz wprowadzić hasło")]
        [DataType(DataType.Password)]
        [Display(Name ="Hasło")]
        public string Password { get; set; }

        [Display(Name ="Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage ="{0} musi mieć co najmniej {2} znaków", MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdż Hasło")]
        [Compare("Password", ErrorMessage ="Hasło i potwierdzenie hasła nie są identyczne")]
        public string ConfirmPassword { get; set; }
    }
}