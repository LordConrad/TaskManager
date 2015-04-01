using System.Web.Mvc;
using System.Web.Security;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using TaskManager.Converters;
using TaskManager.Models;

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
            return View();
        }

        public ActionResult Edit(int id)
        {
            var profileModel = _userService.GetUserById(id);
            if (profileModel == null)
            {
                return null;
            }

            UserViewModel model = new UserViewModel
            {
                UserId = profileModel.UserId,
                Login = profileModel.UserName,
                UserName = profileModel.UserFullName,
                //ChiefId = profileModel.ChiefId.HasValue ? profileModel.ChiefId.Value.ToString() : string.Empty,
                IsAdmin = Roles.IsUserInRole(profileModel.UserName, "Admin"),
                IsChief = Roles.IsUserInRole(profileModel.UserName, "Chief"),
                IsMasterChief = Roles.IsUserInRole(profileModel.UserName, "MasterChief"),
                IsRecipient = Roles.IsUserInRole(profileModel.UserName, "Recipient"),
                IsSender = Roles.IsUserInRole(profileModel.UserName, "Sender")
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            _userService.SaveEditedUser(EntityConverter.ConvertToUserModelBl(model));
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
