using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnipiForum.Areas.Admin.ViewModels;
using UnipiForum.Infrastructure;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.Controllers
{
    //[Authorize(Roles = "admin")]
    [SelectedTab("posts")]
    public class TestsController : Controller
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _tests = context.tests;
                    //Include(t => t.role_users.Select(ru => ru.role)).ToList();


                return View(new TestsIndex
                {
                    Tests = _tests.ToList()
                    

                });

            }
        }

        public ActionResult New()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _tests = context.tests;
                return View(new TestsNew
                {
                   

                });
            }
        }

        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult New(TestsNew form)
        //{
        //    var test = new test();


        //    using (var context = new unipiforumSQLEntities2())
        //    {
        //        var _tests = context.tests.ToList();

        //        //if (_users.Any(u => u.user_university_id == form.University_ID))

        //        //    ModelState.AddModelError("Username", "Username must be unique");

        //        //if (!ModelState.IsValid)
        //        //    return View(form);

        //        user.user_university_id = form.University_ID;
        //        user.email = form.Email;
        //        user.username = form.Username;
        //        user.password_hash = form.Password;


        //        //SyncRoles(form.Roles.ToList(), user.roles.ToList());

        //        //user.SetPassword(form.Password);
        //        context.users.Add(user);
        //        context.SaveChanges();
        //        return RedirectToAction("index");
        //    }
        //}
    }
}  