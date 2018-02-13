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
        public ActionResult AllGroups()
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
        public ActionResult GTG(int grp_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var group = context.groups.Find(grp_id);


                return View(new GTG
                {
                    Group = group


                });

            }
        }
        

        public ActionResult UserGroup(int user_id)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.Find(user_id);
                var urgroup = context.groups.FirstOrDefault(p => p.group_id == user.group_id);
                

                return View(new UsGroup
                {
                    Group = urgroup


                });

            }
        }

    }
}  