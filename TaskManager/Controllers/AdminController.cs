using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.BusinessLogic.Services;
using TaskManager.Converters;
using TaskManager.Models;
using TaskManager.Models.Admin;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

		public AdminController(UserService userService)
        {
			_userService = userService;
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(new AdminManageUsersViewModel
            {
                CurrentUserName = _userService.GetUserById(WebSecurity.CurrentUserId).UserFullName,
                UserRoles = Roles.GetRolesForUser(WebSecurity.CurrentUserName),
                Users = _userService.GetAllUsers().ConvertAll(EntityConverter.Convert)
            });
        }

        public ActionResult Edit(int id)
        {
            var profileModel = _userService.GetUserById(id);
            if (profileModel == null)
            {
                return null;
            }

            UserProfile model = new UserProfile
            {
                UserId = profileModel.UserId,
                UserName = profileModel.UserFullName,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserProfile model)
        {
            _userService.SaveEditedUser(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _userService.DeleteUserById(id);
            return RedirectToAction("Index");
        }

        public ActionResult NewUsersCount()
        {
            var count = _userService.GetNewUsersCount();
            var badge = new BadgeModel {Count = count};
            if (Session["NewUsersCount"] != null && ((int) Session["NewUsersCount"]) < count)
            {
                badge.IsPlay = true;
            }
            Session["NewUsersCount"] = count;

            return PartialView(badge);
        }
    }
}
