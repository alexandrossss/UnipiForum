using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }
        // GET: Auth
        public ActionResult Login()
        {
            return View(new AuthLogin
            {
            });
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            try
            {

        
            using (var context = new unipiforumEntities4())
            {
                var user = context.users.FirstOrDefault(u => u.username == form.Username);
                //var user = form.Username;
                //if (user == null)
                //    UnipiForum.Models.User.FakeHash();

                if (user == null || user.password_hash !=form.Password)
                    ModelState.AddModelError("Username", "Username or password is incorrect");

                if (!ModelState.IsValid)
                    return View(form);

                FormsAuthentication.SetAuthCookie(form.Username, true);

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToRoute("home");

            }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}