using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.ApplicationUser
{
    public class IndexApplicationUserViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string Fullname { get; set; }

        [Display(Name = "Benutzername")]
        public string LogonName { get; set; }

        [Display(Name = "Rollen")]
        public ICollection<string> Roles { get; set; }
    }
}