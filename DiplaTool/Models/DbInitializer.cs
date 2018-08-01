using System;
using System.Data.Entity;
using System.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Useres

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var wegmuellerlu = new ApplicationUser
            {
                Email = "lukas00@bluewin.ch",
                UserName = "wegmuellerlu"
            };

            manager.Create(wegmuellerlu, "Welcome$18");

            #endregion

            #region Subjects

            var dienst1 = new Subject
            {
                Shortcut = "1",
                Name = "Dienst 1",
                Start = Convert.ToDateTime($"{DateTime.Today:d} 07:30"),
                End = Convert.ToDateTime($"{DateTime.Today:d} 12:15"),
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.Yellow
            };

            var dienst2 = new Subject
            {
                Shortcut = "2",
                Name = "Dienst 2",
                Start = Convert.ToDateTime($"{DateTime.Today:d} 12:15"),
                End = Convert.ToDateTime($"{DateTime.Today:d} 17:00"),
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.Yellow
            };

            var frei = new Subject
            {
                Shortcut = "F",
                Name = "Frei",
                Start = Convert.ToDateTime($"{DateTime.Today:d} 07:30"),
                End = Convert.ToDateTime($"{DateTime.Today:d} 17:00"),
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.ForestGreen
            };

            var teilzeit = new Subject
            {
                Shortcut = "T",
                Name = "Ausgleichstag Teilzeit",
                Start = Convert.ToDateTime($"{DateTime.Today:d} 07:30"),
                End = Convert.ToDateTime($"{DateTime.Today:d} 17:00"),
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.Gray
            };

            var homeoffice = new Subject
            {
                Shortcut = "H",
                Name = "Homeoffice",
                Start = Convert.ToDateTime($"{DateTime.Today:d} 07:30"),
                End = Convert.ToDateTime($"{DateTime.Today:d} 17:00"),
                BusyStatus = LegacyFreeBusyStatus.WorkingElsewhere,
                Color = Color.DarkOrange
            };

            context.Subjects.Add(dienst1);
            context.Subjects.Add(dienst2);
            context.Subjects.Add(frei);
            context.Subjects.Add(teilzeit);
            context.Subjects.Add(homeoffice);
            context.SaveChanges();

            #endregion
            
            #region Events

            var eventD1 = new Event
            {
                Subject = dienst1,
                Assignee = wegmuellerlu
            };

            var eventD2 = new Event
            {
                Subject = dienst2,
                Assignee = wegmuellerlu
            };

            var eventFrei = new Event
            {
                Subject = frei,
                Assignee = wegmuellerlu
            };

            var eventTeilzeit = new Event
            {
                Subject = teilzeit,
                Assignee = wegmuellerlu
            };

            var eventHome = new Event
            {
                Subject = homeoffice,
                Assignee = wegmuellerlu
            };

            context.Events.Add(eventD1);
            context.Events.Add(eventD2);
            context.Events.Add(eventFrei);
            context.Events.Add(eventTeilzeit);
            context.Events.Add(eventHome);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}