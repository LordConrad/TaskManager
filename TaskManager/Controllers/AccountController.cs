using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TaskManager.Helpers;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            @ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ModelHelper.GetUserByLogin(model.UserName) != null 
                    && ModelHelper.GetUserByLogin(model.UserName).IsActive 
                    && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new TaskManagerContext())
                {
                    var user =
                        context.Users.FirstOrDefault(
                            x => x.UserName.Equals(model.UserName, StringComparison.InvariantCultureIgnoreCase));
                    if (user != null && user.IsActive)
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                        return View(model);
                    }
                    if (user != null && !user.IsActive)
                    {
                        WebSecurity.CreateAccount(model.UserName, model.Password);
                        user.IsActive = true;
                        context.SaveChanges();
                    }
                    if (user == null)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new
                        {
                            UserFullName = model.UserFullName,
                            IsActive = true
                        });
                    }
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
    }
}
