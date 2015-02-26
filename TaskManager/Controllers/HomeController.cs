using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helpers;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            
            if (ModelHelper.IsMasterChief)
            {
                return RedirectToAction("Index", "MasterChief");
            }
            if (ModelHelper.IsChief)
            {
                return RedirectToAction("Index", "Chief");
            }
            if (ModelHelper.IsRecipient)
            {
                return RedirectToAction("Index", "Recipient");
            }
            if (ModelHelper.IsSender)
            {
                return RedirectToAction("Index", "Sender");
            }
            if (ModelHelper.IsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            
            return View();
        }

        

        

    }
}
