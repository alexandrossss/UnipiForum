 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using UnipiForum.Models;

namespace UnipiForum.Controllers
{
    public class GroupController : Controller
    {
        public ActionResult AssignToGroup(int diffId,int userId)
        {
            using (var context = new unipiforumSQLEntities2())
            {
                var user = context.users.FirstOrDefault(p => p.user_id == userId);                
                var groupsOfThatDiff = context.groups.Where(o => o.diff_id == diffId).ToList();
                var theGroup = 0;
                foreach (var grp in groupsOfThatDiff)
                {
                    if (grp.users.Count <4)
                    {
                        theGroup = grp.group_id;
                    }
                }
                if (theGroup == 0)
                {
                    var nGroup=new group();
                    nGroup.diff_id = diffId;
                    context.groups.Add(nGroup);
                    context.SaveChanges();

                    groupsOfThatDiff= context.groups.Where(o => o.diff_id == diffId).ToList();
                    foreach (var grp in groupsOfThatDiff)
                    {
                        if (grp.users.Count < 4)
                        {
                            theGroup = grp.group_id;
                        }
                    }
                }

                user.group_id = theGroup;
                context.SaveChanges();
                    //here you will assign a user for the first time to a Group

                
                return RedirectToAction("MyProfilePage","UserProfile");
            }
        }

     
    }
}