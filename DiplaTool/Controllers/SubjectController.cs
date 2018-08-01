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
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(_db.Subjects.ToList());
        }
        
        public ActionResult Create()
        {
            return View(new FormSubjectViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormSubjectViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = new Subject
            {
                Shortcut = viewModel.Shortcut,
                Name = viewModel.Name,
                Start = viewModel.Start,
                End = viewModel.End,
                Color = viewModel.Color,
                BusyStatus = viewModel.BusyStatus,
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
                    BusyStatus = subject.BusyStatus
                }
            );
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormSubjectViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var subject = _db.Subjects.Find(viewModel.Id);
            if (subject == null) return View("Error");
            subject.Shortcut = viewModel.Shortcut;
            subject.Name = viewModel.Name;
            subject.Start = viewModel.Start;
            subject.End = viewModel.End;
            subject.Color = viewModel.Color;
            subject.BusyStatus = viewModel.BusyStatus;

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
