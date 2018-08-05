using System;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public virtual Subject Subject { get; set; }

        [Required]
        public virtual ApplicationUser Assignee { get; set; }

        [Required, DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}"), Display(Name = "Datum")]
        public DateTime Date { get; set; }

        public string Body => "Dieser Eintrag wurde automatisch vom DiplaTool generiert.";
    }
}