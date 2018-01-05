 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using NHibernate.Linq;
 using UnipiForum.Areas.Admin.ViewModels;
 using UnipiForum.Infrastructure;
 using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.Session.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {

            });
        }
        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            //var user = new User();
            //SyncRoles(form.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User();
            user.Email = form.Email;
            user.Username = form.Username;
            user.SetPassword(form.Password);

            Database.Session.Save(user);
            return RedirectToAction("index");


        }
    }
}