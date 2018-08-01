using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required, EmailAddress, Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required, StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 8), DataType(DataType.Password), Display(Name = "Passwort")]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Passwort bestätigen"), Compare("Password", ErrorMessage = "Das Passwort entspricht nicht dem Bestätigungspasswort.")]
        public string ConfirmPassword { get; set; }
    }
}