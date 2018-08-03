using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Windows.Forms;
using DiplaTool.Models;
using DiplaTool.ViewModels.Event;
using Microsoft.AspNet.Identity;
using Microsoft.Exchange.WebServices.Data;

namespace DiplaTool.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Events.ToList());
        }

        public ActionResult Dashboard()
        {
            var dashboardEventViewModels = new List<DashboardEventViewModel>();
            var today = DateTime.Today.Date;

            foreach (var user in _db.Users.ToList())
            {
                var dashboardEventViewModel = new DashboardEventViewModel
                {
                    UserName = user.UserName
                };

                foreach (var @event in _db.Events.Where(x => x.Assignee.UserName == user.UserName).ToList())
                {
                    if (@event.Subject.Start.Date == today) dashboardEventViewModel.Events.Add(@event);
                }

                dashboardEventViewModels.Add(dashboardEventViewModel);
            }

            return View(dashboardEventViewModels);
        }

        public ActionResult Create()
        {
            return View(
                new FormEventViewModel
                {
                    Subjects = _db.Subjects.ToList(),
                    Users = _db.Users.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormEventViewModel
                    {
                        Id = viewModel.Id,
                        SubjectId = viewModel.SubjectId,
                        Subjects = _db.Subjects.ToList(),
                        AssigneeId = viewModel.AssigneeId,
                        Users = _db.Users.ToList(),
                        Date = viewModel.Date
                    }
                );

            var @event = new Event
            {
                Subject = _db.Subjects.Find(viewModel.SubjectId),
                Assignee = _db.Users.Find(viewModel.AssigneeId),
                Date = viewModel.Date
            };

            _db.Events.Add(@event);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = _db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(
                new FormEventViewModel
                {
                    Id = @event.Id,
                    SubjectId = @event.Subject.Id,
                    Subjects = _db.Subjects.ToList(),
                    AssigneeId = @event.Assignee.Id,
                    Users = _db.Users.ToList(),
                    Date = @event.Date
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormEventViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormEventViewModel
                    {
                        Id = viewModel.Id,
                        SubjectId = viewModel.SubjectId,
                        Subjects = _db.Subjects.ToList(),
                        AssigneeId = viewModel.AssigneeId,
                        Users = _db.Users.ToList(),
                        Date = viewModel.Date
                    }
                );

            var @event = _db.Events.Find(viewModel.Id);
            if (@event == null) return View("Error");
            @event.Subject = _db.Subjects.Find(viewModel.SubjectId);
            @event.Assignee = _db.Users.Find(viewModel.AssigneeId);
            @event.Date = viewModel.Date;

            _db.Entry(@event).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = _db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var @event = _db.Events.Find(id);
            if (@event == null) return View("Error");
            _db.Events.Remove(@event);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Calendar()
        {
            var userId = User.Identity.GetUserId();
            if (_db.Events.Any(x => x.Assignee.Id == userId)) return View(new CalendarEventViewModel { Events = _db.Events.Where(x => x.Assignee.Id == userId).ToList() });
            return RedirectToAction("Index", "Event");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calendar(CalendarEventViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();

            var exchangeService = new ExchangeService
            {
                Credentials = new WebCredentials(_db.Users.Find(userId)?.Email, viewModel.Password)
            };

            try
            {
                exchangeService.AutodiscoverUrl(_db.Users.Find(userId)?.Email);
            }
            catch
            {
                ModelState.AddModelError("Password", "Das Passwort stimmt nicht.");
                return View(new CalendarEventViewModel { Events = _db.Events.Where(x => x.Assignee.Id == userId).ToList() });
            }

            foreach (var @event in _db.Events.Where(x => x.Assignee.Id == userId).ToList())
            {
                new Appointment(exchangeService)
                {
                    Start = Convert.ToDateTime(@event.Date.Date + @event.Subject.Start.TimeOfDay),
                    End = Convert.ToDateTime(@event.Date.Date.AddDays(@event.Subject.AddDaysToEnd()) + @event.Subject.End.TimeOfDay),
                    Body = @event.Body,
                    Subject = @event.Subject.Name,
                    LegacyFreeBusyStatus = @event.Subject.BusyStatus
                }.Save(SendInvitationsMode.SendOnlyToAll);
            }

            return RedirectToAction("Dashboard", "Event");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
