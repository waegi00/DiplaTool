using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.Models
{
    public class SubjectRole
    {
        public int Id { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}