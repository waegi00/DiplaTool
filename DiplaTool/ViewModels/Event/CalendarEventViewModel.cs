using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiplaTool.ViewModels.Event
{
    public class CalendarEventViewModel
    {
        public int Id { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        public ICollection<Models.Event> Events { get; set; } = new List<Models.Event>();
    }
}