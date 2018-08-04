using System;
using System.Collections.Generic;
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
            #region Roles

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            roleManager.Create(new ApplicationRole("Admin"));
            roleManager.Create(new ApplicationRole("Internal"));
            roleManager.Create(new ApplicationRole("External"));
            roleManager.Create(new ApplicationRole("Apprentice"));

            #endregion

            #region Users

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            #region Instance users

            var wegmuellerlu = new ApplicationUser
            {
                Email = "lukas.wegmueller@post.ch",
                UserName = "lukas.wegmueller@post.ch",
                Firstname = "Lukas",
                Lastname = "Wegmüller",
                LogonName = "wegmuellerlu"
            };

            var cottingma = new ApplicationUser
            {

                Email = "matthias.cotting@post.ch",
                UserName = "matthias.cotting@post.ch",
                Firstname = "Matthias",
                Lastname = "Cotting",
                LogonName = "cottingma"
            };

            var negrip = new ApplicationUser
            {
                Email = "patrick.negri@post.ch",
                UserName = "patrick.negri@post.ch",
                Firstname = "Negri",
                Lastname = "Patrick",
                LogonName = "negrip"
            };

            var hoferfr = new ApplicationUser
            {
                Email = "frederic.hofer@post.ch",
                UserName = "frederic.hofer@post.ch",
                Firstname = "Frédéric",
                Lastname = "Hofer",
                LogonName = "hoferfr"
            };

            var kramerl = new ApplicationUser
            {
                Email = "luca.kramer@post.ch",
                UserName = "luca.kramer@post.ch",
                Firstname = "Luca",
                Lastname = "Kramer",
                LogonName = "kramerl"
            };

            var brudererclau = new ApplicationUser
            {
                Email = "claudio.bruderer@post.ch",
                UserName = "claudio.bruderer@post.ch",
                Firstname = "Claudio",
                Lastname = "Bruderer",
                LogonName = "brudererclau"
            };

            var widmermarc = new ApplicationUser
            {
                Email = "marc.widmer@post.ch",
                UserName = "marc.widmer@post.ch",
                Firstname = "Marc",
                Lastname = "Widmer",
                LogonName = "widmermarc"
            };

            #endregion

            #region Create users

            manager.Create(wegmuellerlu, "Welcome$2018");
            manager.Create(cottingma, "Welcome$2018");
            manager.Create(negrip, "Welcome$2018");
            manager.Create(hoferfr, "Welcome$2018");
            manager.Create(kramerl, "Welcome$2018");
            manager.Create(brudererclau, "Welcome$2018");
            manager.Create(widmermarc, "Welcome$2018");

            #endregion

            #region Add roles to users

            manager.AddToRoles(wegmuellerlu.Id, new string[] { "Apprentice" });
            manager.AddToRoles(cottingma.Id, new string[] { "Apprentice" });
            manager.AddToRoles(negrip.Id, new string[] { "Admin", "Internal" });
            manager.AddToRoles(hoferfr.Id, new string[] { "Admin", "Internal" });
            manager.AddToRoles(kramerl.Id, new string[] { "Internal" });
            manager.AddToRoles(brudererclau.Id, new string[] { "External" });
            manager.AddToRoles(widmermarc.Id, new string[] { "External" });

            #endregion

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
                Color = Color.FromArgb(255, 255, 0),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External"), roleManager.FindByName("Apprentice") }
            };

            var dienst2 = new Subject
            {
                Shortcut = "2",
                Name = "Dienst 2",
                Start = Convert.ToDateTime("12:15"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(255, 255, 0),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External"), roleManager.FindByName("Apprentice") }
            };

            var frei = new Subject
            {
                Shortcut = "F",
                Name = "Frei",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.FromArgb(50, 150, 50),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External"), roleManager.FindByName("Apprentice") }
            };

            var teilzeit = new Subject
            {
                Shortcut = "T",
                Name = "Ausgleichstag Teilzeit",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.Free,
                Color = Color.FromArgb(150, 150, 150),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External") }
            };

            var homeoffice = new Subject
            {
                Shortcut = "H",
                Name = "Homeoffice",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("17:00"),
                IsEndOnNextDay = false,
                BusyStatus = LegacyFreeBusyStatus.WorkingElsewhere,
                Color = Color.FromArgb(255, 150, 0),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External") }
            };

            var pikett = new Subject
            {
                Shortcut = "P",
                Name = "Pikett",
                Start = Convert.ToDateTime("17:00"),
                End = Convert.ToDateTime("07:30"),
                IsEndOnNextDay = true,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(200, 50, 50),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External") }
            };

            var pikettAmWochenende = new Subject
            {
                Shortcut = "PI",
                Name = "Pikett am Wochenende",
                Start = Convert.ToDateTime("07:30"),
                End = Convert.ToDateTime("07:30"),
                IsEndOnNextDay = true,
                BusyStatus = LegacyFreeBusyStatus.Busy,
                Color = Color.FromArgb(200, 50, 50),
                Roles = new List<ApplicationRole> { roleManager.FindByName("Internal"), roleManager.FindByName("External") }
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

            base.Seed(context);
        }
    }
}