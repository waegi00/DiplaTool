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
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("12:15"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(255, 255, 0)
            };

            var dienst2 = new Subject
            {
                Shortcut = "2",
                Name = "Dienst 2",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("12:15"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(255, 255, 0)
            };

            var frei = new Subject
            {
                Shortcut = "F",
                Name = "Frei",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.FromArgb(50, 150, 50)
            };

            var teilzeit = new Subject
            {
                Shortcut = "T",
                Name = "Ausgleichstag Teilzeit",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.FromArgb(150, 150, 150)
            };

            var homeoffice = new Subject
            {
                Shortcut = "H",
                Name = "Homeoffice",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.WorkingElsewhere,
                Color = Color.FromArgb(255, 150, 0)
            };

            var pikett = new Subject
            {
                Shortcut = "P",
                Name = "Pikett",
                Start = Convert.ToDateTime("17:00"),
                End = Convert.ToDateTime("07:30"),
                IsEndOnNextDay = true,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(200, 50, 50)
            };

            context.Subjects.Add(dienst1);
            context.Subjects.Add(dienst2);
            context.Subjects.Add(frei);
            context.Subjects.Add(teilzeit);
            context.Subjects.Add(homeoffice);
            context.Subjects.Add(pikett);
            context.SaveChanges();

            #endregion
            
            #region Events

            var eventD1 = new Event
            {
                Subject = dienst1,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date
            };

            var eventD2 = new Event
            {
                Subject = dienst2,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date.AddDays(1)
            };

            var eventFrei = new Event
            {
                Subject = frei,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date.AddDays(2)
            };

            var eventTeilzeit = new Event
            {
                Subject = teilzeit,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date.AddDays(3)
            };

            var eventHome = new Event
            {
                Subject = homeoffice,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date.AddDays(4)
            };

            var eventtPikett = new Event
            {
                Subject = homeoffice,
                Assignee = wegmuellerlu,
                Date = DateTime.Today.Date.AddDays(4)
            };

            context.Events.Add(eventD1);
            context.Events.Add(eventD2);
            context.Events.Add(eventFrei);
            context.Events.Add(eventTeilzeit);
            context.Events.Add(eventHome);
            context.Events.Add(eventtPikett);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}