using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;

namespace DiplaTool.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Shortcut { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [Required, DataType(DataType.Time)]
        public DateTime End { get; set; }

        [Required]
        public bool IsEndOnNextDay { get; set; }

        public int AddDaysToEnd() => Convert.ToInt32(IsEndOnNextDay);

        [Required]
        public LegacyFreeBusyStatus BusyStatus { get; set; }

        [Browsable(false), Column("Color")]
        public int Argb
        {
            get => _myColor.ToArgb();
            set => _myColor = Color.FromArgb(value);
        }

        private Color _myColor;

        [NotMapped]
        public Color Color
        {
            get => _myColor;
            set => _myColor = value;
        }

        public string ColorToString()
        {
            return $"#{_myColor.R:X2}{_myColor.G:X2}{_myColor.B:X2}";
        }

        public virtual ICollection<Event> Events { get; set; }
    }
}