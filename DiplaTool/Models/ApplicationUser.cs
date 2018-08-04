using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplaTool.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "Vorname")]
        public string Firstname { get; set; }

        [Required, Display(Name = "Nachname")]
        public string Lastname { get; set; }

        [Display(Name = "Name")]
        public string Fullname => $"{Firstname} {Lastname}";

        [Required, Display(Name = "Benutzername")]
        public string LogonName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Beachten Sie, dass der "authenticationType" mit dem in "CookieAuthenticationOptions.AuthenticationType" definierten Typ übereinstimmen muss.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Benutzerdefinierte Benutzeransprüche hier hinzufügen
            return userIdentity;
        }
    }
}