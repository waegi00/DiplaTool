using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Passwort")]
        public string Password { get; set; }

        [Display(Name = "Passwort merken?")]
        public bool RememberMe { get; set; }
    }
}