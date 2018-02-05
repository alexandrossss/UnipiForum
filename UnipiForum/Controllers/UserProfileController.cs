﻿using System;
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
    }
}
