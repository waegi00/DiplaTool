﻿using System.Collections.Generic;

namespace DiplaTool.ViewModels.Event
{
    public class DashboardEventViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public ICollection<Models.Event> Events { get; set; } = new List<Models.Event>();
    }
}