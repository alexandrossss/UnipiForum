using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UnipiForum.Areas.Admin.Controllers;
using UnipiForum.Controllers;

namespace UnipiForum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new[] { typeof(GroupController).Namespace };
            //var chatNamespace = new[] { typeof(ChatController).Namespace };


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);
            routes.MapRoute("UserTests", "usertests", new { controller = "UserTests", action = "Index" }, namespaces);
            routes.MapRoute("Auth", "auth", new { controller = "Auth", action = "Lazarakis" }, namespaces);

            routes.MapRoute("AssignToGroup", "assigntogroup", new { controller = "Group", action = "AssignToGroup" }, namespaces);

            routes.MapRoute("NewUserProfile", "newuserprofile", new { controller = "UserProfile", action = "NewUser" }, namespaces);
            routes.MapRoute("UserProfile", "userprofile", new { controller = "UserProfile", action = "MyProfilePage" }, namespaces);
            routes.MapRoute("UsGroup", "usgroup", new { controller = "Users", action = "UserGroup" }, namespaces);

            routes.MapRoute("UserProfileResults", "userprofileresults", new { controller = "UserProfile", action = "Results" }, namespaces);
            routes.MapRoute("Results", "results", new { controller = "Users", action = "Results" }, namespaces);

            routes.MapRoute("UserGroup", "usergroup", new { controller = "UserProfile", action = "UsGroup" }, namespaces);

            routes.MapRoute("UploadFile", "uploadfile", new {controller = "UploadFile", action = "Index"}, namespaces);

            routes.MapRoute("UploadFiles", "uploadfiles", new { controller = "Home", action = "UploadFiles" }, namespaces);
            routes.MapRoute("UploadProject", "uploadproject", new { controller = "Home", action = "UploadProject" }, namespaces);

            routes.MapRoute("ProjectPage", "projectpage", new { controller = "Home", action = "ProjectPage" }, namespaces);


            //routes.MapRoute("Chat", "GetChatMessages", new {controller = "Chat", action = "GetChatMessages"}, chatNamespace);

            routes.MapRoute("About", "about", new { controller = "Home", action = "About" }, namespaces);
        }
    }
}
