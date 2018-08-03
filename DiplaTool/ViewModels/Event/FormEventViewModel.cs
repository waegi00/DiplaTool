using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Event
{
    public class FormEventViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Dienst")]
        public int SubjectId { get; set; }

        public ICollection<Models.Subject> Subjects { get; set; } = new List<Models.Subject>();

        [Required, Display(Name = "Mitarbeiter")]
        public string AssigneeId { get; set; }

        public ICollection<Models.ApplicationUser> Users { get; set; } = new List<Models.ApplicationUser>();

        [Required, DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}"), Display(Name = "Datum")]
        public DateTime Date { get; set; }
    }
}