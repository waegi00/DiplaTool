using System;
using System.Data.Entity;
using System.Drawing;
using DiplaTool.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.Models
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Roles

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Admin"));
            roleManager.Create(new IdentityRole("Internal"));
            roleManager.Create(new IdentityRole("External"));
            roleManager.Create(new IdentityRole("Apprentice"));

            #endregion

            #region Useres

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var wegmuellerlu = new ApplicationUser
            {
                Email = "lukas.wegmueller@outlook.com",
                UserName = "lukas.wegmueller@outlook.com",
                Firstname = "Lukas",
                Lastname = "Wegmüller",
                LogonName = "wegmuellerlu"
            };

            manager.Create(wegmuellerlu, "Welcome$18");
            manager.AddToRole(wegmuellerlu.Id, "Admin");

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
                Start = Convert.ToDateTime("12:15"),
                End = Convert.ToDateTime("17:00"),
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

            var pikettAmWochenende = new Subject
            {
                Shortcut = "PI",
                Name = "Pikett am Wochenende",
                Start = Convert.ToDateTime("07:30"),
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
            context.Subjects.Add(pikettAmWochenende);
            context.SaveChanges();

            #endregion

            #region Events

            var montag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikett,
                Date = new DateTime(2018, 07, 30)
            };

            var dienstag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikett,
                Date = new DateTime(2018, 07, 31)
            };

            var mittwoch = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikett,
                Date = new DateTime(2018, 08, 01)
            };

            var donnerstag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikett,
                Date = new DateTime(2018, 08, 02)
            };

            var freitag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikett,
                Date = new DateTime(2018, 08, 03)
            };

            var samstag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikettAmWochenende,
                Date = new DateTime(2018, 08, 04)
            };

            var sonntag = new Event
            {
                Assignee = wegmuellerlu,
                Subject = pikettAmWochenende,
                Date = new DateTime(2018, 08, 05)
            };

            context.Events.Add(montag);
            context.Events.Add(dienstag);
            context.Events.Add(mittwoch);
            context.Events.Add(donnerstag);
            context.Events.Add(freitag);
            context.Events.Add(samstag);
            context.Events.Add(sonntag);
            context.SaveChanges();

            #endregion

            base.Seed(context);
        }
    }
}