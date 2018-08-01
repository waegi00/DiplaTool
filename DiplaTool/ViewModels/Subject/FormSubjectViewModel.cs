using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.ViewModels.Subject
{
    public class FormSubjectViewModel
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

        public ICollection<LegacyFreeBusyStatus> BusyStatuses { get; set; } = new List<LegacyFreeBusyStatus>
        {
            LegacyFreeBusyStatus.Busy,
            LegacyFreeBusyStatus.Free,
            LegacyFreeBusyStatus.OOF,
            LegacyFreeBusyStatus.Tentative,
            LegacyFreeBusyStatus.WorkingElsewhere,
            LegacyFreeBusyStatus.NoData
        };

        [Required]
        public Color Color { get; set; }
    }
}