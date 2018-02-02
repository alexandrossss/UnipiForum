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
    }
}
