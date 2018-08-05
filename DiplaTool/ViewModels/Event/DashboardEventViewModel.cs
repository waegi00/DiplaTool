using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Event
{
    public class DashboardEventViewModel
    {
        public int Id { get; set; }
        
        public Dictionary<string, Dictionary<DateTime, ICollection<Models.Event>>> Events { get; set; } = new Dictionary<string, Dictionary<DateTime, ICollection<Models.Event>>>();

        [Display(Name = "Dienstcheck")]
        public ICollection<bool> DienstChecks { get; set; } = new List<bool>();

        [Display(Name = "Pikettcheck")]
        public ICollection<bool> PikettChecks { get; set; } =  new List<bool>();

        public string GetColor(bool value)
        {
            return value ? "#00FF00" : "#FF0000";
        }
    }
}