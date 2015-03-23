using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Services;
using TaskManager.Helpers;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService = new UserService();

        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            

            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            if (userService.IsMasterChief())
            {
                return RedirectToAction("Index", "MasterChief");
            }
            if (userService.IsChief())
            {
                return RedirectToAction("Index", "Chief");
            }
            if (userService.IsRecipient())
            {
                return RedirectToAction("Index", "Recipient");
            }
            if (userService.IsSender())
            {
                return RedirectToAction("Index", "Sender");
            }
            if (userService.IsAdmin())
            {
                return RedirectToAction("Index", "Admin");
            }
            
            return View();
        }

        

        

    }
}
