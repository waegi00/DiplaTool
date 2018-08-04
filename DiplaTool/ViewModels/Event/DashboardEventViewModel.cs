using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Event
{
    public class DashboardEventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Benutzername")]
        public string UserName { get; set; }

        public Dictionary<DateTime, ICollection<Models.Event>> Events { get; set; } = new Dictionary<DateTime, ICollection<Models.Event>>();
    }
}