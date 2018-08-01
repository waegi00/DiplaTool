using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Shortcut { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public LegacyFreeBusyStatus BusyStatus { get; set; }

        [Required]
        public Color Color { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}