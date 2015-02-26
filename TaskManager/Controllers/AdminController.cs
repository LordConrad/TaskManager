using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using TaskManager.Helpers;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var profileModel = ModelHelper.GetUserById(id);
            if (profileModel == null)
            {
                return null;
            }
            


            UserModel model = new UserModel
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
        public ActionResult Edit(UserModel model)
        {
            ModelHelper.SaveEditedUser(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ModelHelper.DeleteUserById(id);
            return RedirectToAction("Index");
        }

        public ActionResult NewUsersCount()
        {
            var count = ModelHelper.GetNewUsersCount();
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
