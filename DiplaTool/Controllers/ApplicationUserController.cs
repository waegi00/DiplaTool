﻿using System;
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
            return View(_userManager.Users.ToList());
        }

        public ActionResult Create()
        {
            return View(
                new FormApplicationUserViewModel
                {
                    AllRoles = _roleManager.Roles.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormApplicationUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormApplicationUserViewModel
                    {
                        Id = viewModel.Id,
                        Email = viewModel.Email,
                        Firstname = viewModel.Firstname,
                        Lastname = viewModel.Lastname,
                        LogonName = viewModel.LogonName,
                        Roles = viewModel.Roles,
                        AllRoles = _roleManager.Roles.ToList()
                    }
                );

            var applicationUser = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                LogonName = viewModel.LogonName
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
                new FormApplicationUserViewModel()
                {
                    Email = applicationUser.Email,
                    Firstname = applicationUser.Firstname,
                    Lastname = applicationUser.Lastname,
                    LogonName = applicationUser.LogonName,
                    Roles = _roleManager.Roles.Where(x => x.Users.Any(y => y.UserId == applicationUser.Id)).Select(x => x.Name).ToList(),
                    AllRoles = _roleManager.Roles.ToList()
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormApplicationUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(
                    new FormApplicationUserViewModel
                    {
                        Id = viewModel.Id,
                        Email = viewModel.Email,
                        Firstname = viewModel.Firstname,
                        Lastname = viewModel.Lastname,
                        LogonName = viewModel.LogonName,
                        Roles = viewModel.Roles,
                        AllRoles = _roleManager.Roles.ToList()
                    }
                );

            var applicationUser = new ApplicationUser
            {
                Email = viewModel.Email,
                UserName = viewModel.Email,
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                LogonName = viewModel.LogonName
            };

            //Update user
            _userManager.Update(applicationUser);

            //Remove from all roles
            _userManager.RemoveFromRoles(applicationUser.Id, _userManager.GetRoles(applicationUser.Id).ToArray());

            //Add selected roles
            foreach (var role in viewModel.Roles)
            {
                _userManager.AddToRoles(applicationUser.Id, role);
            }

            return RedirectToAction("Index");
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var applicationUser = _userManager.FindById(id);
            if (applicationUser == null) return View("Error");
            _userManager.RemoveLogin(applicationUser.Id,new UserLoginInfo("Wadu", "heck"));
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
