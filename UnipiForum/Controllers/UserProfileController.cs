using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Controllers
{
    public class UserProfileController : Controller
    {

        public ActionResult NewUser()
        {
            return View(new SighUp
            {

            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult NewUser(SighUp form)
        {
            var user = new user();


            using (var context = new unipiforumSQLEntities2())
            {
                var _users = context.users.ToList();
                


                if (_users.Any(u => u.user_university_id == form.University_ID))

                    ModelState.AddModelError("University_ID", "University ID must be unique");

                if (_users.Any(u => u.username == form.Username))

                    ModelState.AddModelError("Username", "Username already exist");

                if (!ModelState.IsValid)
                    return View(form);

                

                user.user_university_id = form.University_ID;
                user.email = form.Email;
                user.username = form.Username;
                user.password_hash = form.Password;
                context.users.Add(user);
                context.SaveChanges();
                var roleus = new role_users();
                roleus.role_id = context.roles.FirstOrDefault(p => p.name == "student").role_id;
                roleus.user_id = context.users.FirstOrDefault(o => o.username == form.Username).user_id;
                context.role_users.Add(roleus);
                context.SaveChanges();
                var auth = new AuthLogin()
                {
                    Username = form.Username,
                    Password = form.Password
                };


                return RedirectToAction("Lazarakis", "Auth", auth);//new{form = auth}

            }
        }

        public ActionResult MyProfilePage()
        {
            using (var context = new unipiforumSQLEntities2())
            {

                return View(new MyProfileViewModel
                {
                    Users = context.users.FirstOrDefault(g => g.username == User.Identity.Name)
                });
            }
        }

        public ActionResult Results()
        {
            using (var context = new unipiforumSQLEntities2())
            {

                var usid = context.users.FirstOrDefault(h => h.username == User.Identity.Name)?.user_id;
                return View(new ResultsViewModel()
                {
                    Results = context.results.Where(g => g.user_id == usid).ToList()
                });

            }
        }

        public ActionResult UsGroup()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.FirstOrDefault(i => i.username == User.Identity.Name);
                var group = context.groups.Include("users").FirstOrDefault(o=>o.group_id== user.group_id);
                return View(new UserGroup()
                {
                    Group = group
                });
            }
        }
    }
}
