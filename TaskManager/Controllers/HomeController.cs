using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using TaskManager.Helpers;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

		public HomeController(UserService userService)
        {
			_userService = userService;
        }

        //
        // GET: /Home/
        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
			if (_userService.IsMasterChief())
            {
                return RedirectToAction("Index", "MasterChief");
            }
			if (_userService.IsChief())
            {
                return RedirectToAction("Index", "Chief");
            }
			if (_userService.IsRecipient())
            {
                return RedirectToAction("Index", "Recipient");
            }
			if (_userService.IsSender())
            {
                return RedirectToAction("Index", "Sender");
            }
			if (_userService.IsAdmin())
            {
                return RedirectToAction("Index", "Admin");
            }          
            return View();
        }
    }
}
