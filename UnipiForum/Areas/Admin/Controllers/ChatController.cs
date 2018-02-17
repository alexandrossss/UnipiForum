using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ChatController : Controller
    {
        
    }
}