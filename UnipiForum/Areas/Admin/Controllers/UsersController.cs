 using System;
using System.Collections.Generic;
 using System.Data.Entity;
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
            using (var context = new unipiforumEntities2())
            {
                var _users = context.users;


                return View(new UsersIndex
                {
                    Users = _users.ToList()
                    //Database.Session.Query<User>().ToList()

                });

            }
        }

        public ActionResult New()
        {
            using (var context = new unipiforumEntities2())
            {
                var _roles = context.roles;
                return View(new UsersNew
                {
                    Roles = _roles.Select(role => new RoleCheckbox
                    {
                        Id = role.role_id,
                        IsChecked = false,
                        Name = role.name
                    }).ToList()

                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            var user = new user();
            SyncRoles(form.Roles, user.Roles);

            using (var context = new unipiforumEntities2())
            {
                var _users = context.users;

                if (_users.Any(u => u.username == form.Username))

                    ModelState.AddModelError("Username", "Username must be unique");

                if (!ModelState.IsValid)
                    return View(form);

                //user.user_id = form.Id;
                user.email = form.Email;
                user.username = form.Username;
                user.SetPassword(form.Password);
                context.SaveChanges();
                return RedirectToAction("index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var context = new unipiforumEntities2())
            {
                var user = context.users.Find(id);
                //var user = Database.Session.Load<User>(id);

                if (user == null)
                    return HttpNotFound();
                
                return View(new UsersEdit
                {
                    //UserId = user.UserId,
                    Username = user.username,
                    Email = user.email,

                    Roles = context.roles.Select(role => new RoleCheckbox
                    {
                        Id = role.role_id,
                        IsChecked = user.Roles.Contains(role),
                        Name = role.name
                    }).ToList()



                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersEdit form)
        {
            using (var context = new unipiforumEntities2())
            {


                var user = context.users.Find(id);
                if (user == null)
                    return HttpNotFound();

                SyncRoles(form.Roles, user.Roles);

                if (context.users.Any(u => u.username == form.Username && u.user_id != id))
                    ModelState.AddModelError("Username", "Username must be unique");

                if (!ModelState.IsValid)
                    return View(form);

                //user.UserId = form.UserId;
                user.username= form.Username;
                user.email = form.Email;
                context.SaveChanges();
                

                return RedirectToAction("index");
            }
        }

        public ActionResult ResetPassword(int id)
        {
            using (var context = new unipiforumEntities2())
            {
                var user = context.users.Find(id);
                if (user == null)
                    return HttpNotFound();

                return View(new UsersResetPassword
                {
                    Username = user.username
                });
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            using (var context = new unipiforumEntities2())
            {
                var user = context.users.Find(id);
                if (user == null)
                    return HttpNotFound();

                form.Username = user.username;

                if (!ModelState.IsValid)
                    return View(form);

                user.SetPassword(form.Password);

                context.SaveChanges();
                

                return RedirectToAction("index");

            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new unipiforumEntities2())
            {

                var user = context.users.Find(id);
                if (user == null)
                    return HttpNotFound();

                Database.Session.Delete(user);
                return RedirectToAction("index");
            }
        }

        private void SyncRoles(IList<RoleCheckbox> checkboxes, IList<role> roles)
        {
            var selectedRoles = new List<role>();
            using (var context = new unipiforumEntities2())
            {
                var _roles = context.roles;
                foreach (var role in _roles)
                {
                    var checkbox = checkboxes.Single(c => c.Id == role.role_id);
                    checkbox.Name = role.name;

                    if (checkbox.IsChecked)
                        selectedRoles.Add(role);
                }

                foreach (var toadd in selectedRoles.Where(t => !roles.Contains(t)))
                    roles.Add(toadd);

                foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                    roles.Remove(toRemove);

            }
        }
    }
}