using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DiplaTool.Models;
using DiplaTool.ViewModels.Subject;

namespace DiplaTool.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        //Default context
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(_db.Subjects.OrderBy(x => x.Name).ToList());
        }

        public ActionResult Create()
        {
            return View(
                new FormSubjectViewModel
                {
                    AllRoles = _db.SubjectRoles.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormSubjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormSubjectViewModel
                    {
                        Shortcut = viewModel.Shortcut,
                        Name = viewModel.Name,
                        Start = viewModel.Start,
                        End = viewModel.End,
                        Color = viewModel.Color,
                        BusyStatus = viewModel.BusyStatus,
                        IsEndOnNextDay = viewModel.IsEndOnNextDay,
                        Roles = viewModel.Roles,
                        AllRoles = _db.SubjectRoles.ToList()
                    }
                );

            if (!viewModel.IsEndOnNextDay && viewModel.Start >= viewModel.End)
            {
                ModelState.AddModelError("Start", "Die Startzeit muss vor der Endzeit sein.");
                return View(viewModel);
            }

            var subject = new Subject
            {
                Shortcut = viewModel.Shortcut,
                Name = viewModel.Name,
                Start = viewModel.Start,
                End = viewModel.End,
                Color = viewModel.Color,
                BusyStatus = viewModel.BusyStatus,
                IsEndOnNextDay = viewModel.IsEndOnNextDay,
                SubjectRoles = _db.SubjectRoles.Where(x => viewModel.Roles.Contains(x.Name)).ToList()
            };

            _db.Subjects.Add(subject);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(
                new FormSubjectViewModel
                {
                    Id = subject.Id,
                    Shortcut = subject.Shortcut,
                    Name = subject.Name,
                    Start = subject.Start,
                    End = subject.End,
                    Color = subject.Color,
                    BusyStatus = subject.BusyStatus,
                    IsEndOnNextDay = subject.IsEndOnNextDay,
                    Roles = _db.SubjectRoles.Where(x => x.Subjects.Select(s => s.Id).Contains(subject.Id)).Select(x => x.Name).ToList(),
                    AllRoles = _db.SubjectRoles.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormSubjectViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormSubjectViewModel
                    {
                        Shortcut = viewModel.Shortcut,
                        Name = viewModel.Name,
                        Start = viewModel.Start,
                        End = viewModel.End,
                        Color = viewModel.Color,
                        BusyStatus = viewModel.BusyStatus,
                        IsEndOnNextDay = viewModel.IsEndOnNextDay,
                        Roles = viewModel.Roles,
                        AllRoles = _db.SubjectRoles.ToList()
                    }
                );

            if (!viewModel.IsEndOnNextDay && viewModel.Start >= viewModel.End)
            {
                ModelState.AddModelError("Start", "Die Startzeit muss vor der Endzeit sein.");
                return View(
                    new FormSubjectViewModel
                    {
                        Shortcut = viewModel.Shortcut,
                        Name = viewModel.Name,
                        Start = viewModel.Start,
                        End = viewModel.End,
                        Color = viewModel.Color,
                        BusyStatus = viewModel.BusyStatus,
                        IsEndOnNextDay = viewModel.IsEndOnNextDay,
                        Roles = viewModel.Roles,
                        AllRoles = _db.SubjectRoles.ToList()
                    }
                );
            }

            var subject = _db.Subjects.Find(viewModel.Id);
            if (subject == null) return View("Error");
            subject.Shortcut = viewModel.Shortcut;
            subject.Name = viewModel.Name;
            subject.Start = viewModel.Start;
            subject.End = viewModel.End;
            subject.Color = viewModel.Color;
            subject.BusyStatus = viewModel.BusyStatus;
            subject.IsEndOnNextDay = viewModel.IsEndOnNextDay;
            subject.SubjectRoles = _db.SubjectRoles.Where(x => viewModel.Roles.Contains(x.Name)).ToList();

            _db.Entry(subject).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subject = _db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var subject = _db.Subjects.Find(id);
            if (subject == null) return View("Error");
            _db.Subjects.Remove(subject);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
