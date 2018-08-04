using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DiplaTool.Models;
using DiplaTool.ViewModels.ApplicationUser;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplaTool.Controllers
{
    [Authorize]
    public class ApplicationUserController : Controller
    {
        //DbContext user for disposing
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        //Context and managers used for the CUD
        private static readonly ApplicationDbContext Db = new ApplicationDbContext();
        private readonly ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
        private readonly RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Db));
        
        public ActionResult Index()
        {
            var users = new List<IndexApplicationUserViewModel>();

            _userManager.Users.ToList().ForEach(user =>
                users.Add(
                    new IndexApplicationUserViewModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Fullname = $"{user.Firstname} {user.Lastname}",
                        UserName = user.UserName,
                        Roles = _userManager.GetRoles(user.Id)
                    }
                )
            );

            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(
                new FormApplicationUserViewModel
                {
                    AllRoles = _roleManager.Roles.ToList()
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormApplicationUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormApplicationUserViewModel
                    {
                        Email = viewModel.Email,
                        Firstname = viewModel.Firstname,
                        Lastname = viewModel.Lastname,
                        UserName = viewModel.UserName,
                        Roles = viewModel.Roles,
                        AllRoles = _roleManager.Roles.ToList()
                    }
                );

            var applicationUser = new ApplicationUser
            {
                Email = viewModel.Email,
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                UserName = viewModel.UserName
            };

            //Create user
            _userManager.Create(applicationUser, $"Welcome${DateTime.Today.Year.ToString()}");

            //Add selected roles
            foreach (var role in viewModel.Roles)
            {
                _userManager.AddToRole(applicationUser.Id, role);
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(
                new FormApplicationUserViewModel
                {
                    Id = new Guid(),
                    Email = applicationUser.Email,
                    Firstname = applicationUser.Firstname,
                    Lastname = applicationUser.Lastname,
                    UserName = applicationUser.UserName,
                    Roles = _userManager.GetRoles(applicationUser.Id),
                    AllRoles = _roleManager.Roles.ToList()
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormApplicationUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormApplicationUserViewModel
                    {
                        Email = viewModel.Email,
                        Firstname = viewModel.Firstname,
                        Lastname = viewModel.Lastname,
                        UserName = viewModel.UserName,
                        Roles = viewModel.Roles,
                        AllRoles = _roleManager.Roles.ToList()
                    }
                );

            var applicationUser = new ApplicationUser
            {
                Email = viewModel.Email,
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                UserName = viewModel.UserName
            };

            //Update user
            _userManager.Update(applicationUser);

            //Remove from all roles
            _userManager.RemoveFromRoles(_userManager.FindByEmail(applicationUser.Email).Id, _userManager.GetRoles(_userManager.FindByEmail(applicationUser.Email).Id).ToArray());

            //Add selected roles
            foreach (var role in viewModel.Roles)
            {
                _userManager.AddToRoles(_userManager.FindByEmail(applicationUser.Email).Id, role);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null) return View("Error");
            _userManager.Delete(applicationUser);
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
