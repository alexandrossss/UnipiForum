using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnipiForum.Models;

namespace UnipiForum.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult ProjectPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProject()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                bool isSuccess = false;
                string serverMessage = string.Empty;
                var fileOne = Request.Files[0] as HttpPostedFileBase;
                
                string uploadPath =Server.MapPath(".\\Files"); 
                var newfilename =$"project{context.users.FirstOrDefault(p => p.username == User.Identity.Name).user_university_id}.rar";
                string newFileOne = Path.Combine(uploadPath, newfilename);
                

                fileOne.SaveAs(newFileOne);
                

                if (System.IO.File.Exists(newFileOne)) 
                {
                    isSuccess = true;
                    serverMessage = "Files have been uploaded successfully";
                }
                else
                {
                    isSuccess = false;
                    serverMessage = "Files upload is failed. Please try again.";
                }


                return Json(new { IsSucccess = isSuccess, ServerMessage = serverMessage }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadFiles()
        {
            using (var context = new unipiforumSQLEntities2())
            {
                bool isSuccess = false;
                string serverMessage = string.Empty;
                var fileOne = Request.Files[0] as HttpPostedFileBase;
                
                string uploadPath =Server.MapPath(".\\Files"); 
                var newfilename =$"user{context.users.FirstOrDefault(p => p.username == User.Identity.Name).user_id}.jpg";
                string newFileOne = Path.Combine(uploadPath, newfilename);
                

                fileOne.SaveAs(newFileOne);
                

                if (System.IO.File.Exists(newFileOne)) 
                {
                    isSuccess = true;
                    serverMessage = "Files have been uploaded successfully";
                }
                else
                {
                    isSuccess = false;
                    serverMessage = "Files upload is failed. Please try again.";
                }

                return Json(new {IsSucccess = isSuccess, ServerMessage = serverMessage}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}