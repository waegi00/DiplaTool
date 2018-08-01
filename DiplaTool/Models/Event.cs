namespace DiplaTool.Models
{
    public class Event
    {
        public int Id { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ApplicationUser Assignee { get; set; }

        public string Body => "Dieser Eintrag wurde automatisch vom DiplaTool generiert.";
    }
}