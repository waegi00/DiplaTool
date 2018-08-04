using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplaTool.ViewModels.ApplicationUser
{
    public class FormApplicationUserViewModel
    {
        public string Id { get; set; }

        [Required, DataType(DataType.EmailAddress), Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Initialpasswort")]
        public string PasswordHint => $"Welcome${DateTime.Today.Year.ToString()}";

        [Required, Display(Name = "Vorname")]
        public string Firstname { get; set; }

        [Required, Display(Name = "Nachname")]
        public string Lastname { get; set; }

        [Required, Display(Name = "Benutzername")]
        public string UserName { get; set; }

        [Required, Display(Name = "Rollen")]
        public ICollection<string> Roles { get; set; }

        public ICollection<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();
    }
}