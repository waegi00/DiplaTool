using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplaTool.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public ApplicationRole() : base()
        {
        }

        public ICollection<Subject> Subjects { get; set; }
    }
}