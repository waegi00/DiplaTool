using System.Collections.Generic;

namespace DiplaTool.ViewModels.Event
{
    public class FormEventViewModel
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public ICollection<Models.Subject> Subjects { get; set; } = new List<Models.Subject>();

        public string AssigneeId { get; set; }

        public ICollection<Models.ApplicationUser> Users { get; set; } = new List<Models.ApplicationUser>();
    }
}