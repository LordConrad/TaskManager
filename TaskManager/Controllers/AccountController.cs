using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        public AccountController(UserService userService)
        {
			_userService = userService;
        }

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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userService.GetUserByLogin(model.UserName) != null
                    && _userService.GetUserByLogin(model.UserName).IsActive 
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
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.CheckUser(model.UserName);

                    if (user != null && user.IsActive)
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                        return View(model);
                    }
                    if (user != null && !user.IsActive)
                    {
                        WebSecurity.CreateAccount(model.UserName, model.Password);
                        user.IsActive = true;
                        //userService.SaveChanges();
                        
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
            return View(model);
        }
    }
}
