using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Event
{
    public class FormEventViewModel
    {
        public int Id { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public ICollection<Models.Subject> Subjects { get; set; } = new List<Models.Subject>();

        [Required]
        public string AssigneeId { get; set; }

        public ICollection<Models.ApplicationUser> Users { get; set; } = new List<Models.ApplicationUser>();

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}