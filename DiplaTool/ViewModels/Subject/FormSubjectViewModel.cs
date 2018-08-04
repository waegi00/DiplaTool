using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.ViewModels.Subject
{
    public class FormSubjectViewModel
    {
        public int Id { get; set; }

        [Required, Display(Name = "Kürzel")]
        public string Shortcut { get; set; }

        [Required, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, DataType(DataType.Time), Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required, DataType(DataType.Time), Display(Name = "Ende")]
        public DateTime End { get; set; }

        [Required, Display(Name = "Ende am nächsten Tag")]
        public bool IsEndOnNextDay { get; set; }

        [Required, Display(Name = "Anzeigen als")]
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

        [Required, Display(Name = "Farbe")]
        public Color Color { get; set; }

        [Required, Display(Name = "Rollen")]
        public ICollection<string> Roles { get; set; }

        public ICollection<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();
    }
}