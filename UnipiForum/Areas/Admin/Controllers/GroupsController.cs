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
    [Authorize(Roles = "admin")]
    [SelectedTab("groups")]
    public class GroupsController : Controller
    {
        // GET: Admin/Posts
        public ActionResult AllGroups()// Groups page for admin
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var _groups = context.groups.Include("users");


                return View(new GroupsIndex
                {
                    Groups = _groups.ToList()
                    

                });

            }
        }
        public ActionResult GTG(int group_id)// Go to Group when you pick a group from groups page
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var group = context.groups.Include("users").FirstOrDefault(p => p.group_id == group_id);


                return View(new GTG
                {
                    Group = group


                });

            }
        }
        

        

    }
}  