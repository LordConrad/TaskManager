using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using TaskManager.DataAccess.Interfaces;
using WebMatrix.WebData;

namespace TaskManager.DataAccess.Providers
{
    public class UserProvider : IUserProvider
    {
        public static string[] RolesArray = { "Admin", "Sender", "Recipient", "Chief", "MasterChief" };

        public UserProfile CurrentUser()
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var curUser = context.Users.FirstOrDefault(x => x.UserId == WebSecurity.CurrentUserId);
                    if (curUser != null)
                    {
                        return curUser;
                    }
                }
            }
            catch (Exception)
            {
                WebSecurity.Logout();
            }
            return null;
        }

        public bool IsAdmin()
        {
            try
            {
                return Roles.IsUserInRole("Admin");
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }

        public bool IsChief()
        {
            try
            {
                return Roles.IsUserInRole("Chief");
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }

        public bool IsRecipient()
        {

            try
            {
                return Roles.IsUserInRole("Recipient");
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }

        public bool IsSender()
        {
            try
            {
                return Roles.IsUserInRole("Sender");
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }

        public bool IsMasterChief()
        {
            try
            {
                return Roles.IsUserInRole("MasterChief");
            }
            catch (Exception)
            {
                WebSecurity.Logout();
                return false;
            }
        }

        public List<UserProfile> GetAllUsers()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Users.ToList();
            }
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            var roles = Roles.GetRolesForUser(userName).ToList();
            foreach (var role in roles)
            {
                switch (role)
                {
                    case "Chief": yield return "Руководитель"; break;
                    case "Sender": yield return "Отправитель"; break;
                    case "Recipient":
                        yield return "Исполнитель"; break;
                    case "Admin":
                        yield return "Администратор"; break;
                    case "MasterChief":
                        yield return "Главврач"; break;
                }
            }
        }

        public UserProfile GetUserByLogin(string login)
        {
            UserProfile model;
            using (var context = new TaskManagerContext())
            {
                model =
                    context.Users.FirstOrDefault(
                        x => x.UserName.Equals(login, StringComparison.InvariantCultureIgnoreCase));
            }
            return model;
        }

        public UserProfile GetUserById(int id)
        {
            UserProfile model;
            using (var context = new TaskManagerContext())
            {
                model = context.Users.FirstOrDefault(x => x.UserId == id);
            }
            return model;
        }

        private string[] GetRolesNamesArray(UserModel model)
        {
            List<string> list = new List<string>();
            if (model.IsAdmin) list.Add("Admin");
            if (model.IsChief) list.Add("Chief");
            if (model.IsMasterChief) list.Add("MasterChief");
            if (model.IsRecipient) list.Add("Recipient");
            if (model.IsSender) list.Add("Sender");
            return list.ToArray();
        }

        public bool SaveEditedUser(UserModel model)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var editedUser = context.Users.FirstOrDefault(x => x.UserId == model.UserId);
                    if (editedUser != null)
                    {
                        var curRoles = Roles.GetRolesForUser(editedUser.UserName);
                        if (curRoles.Length > 0)
                        {
                            Roles.RemoveUserFromRoles(editedUser.UserName, curRoles);
                        }
                        var newRoles = GetRolesNamesArray(model);
                        if (newRoles.Length > 0)
                        {
                            Roles.AddUserToRoles(editedUser.UserName, newRoles);
                        }
                        editedUser.UserName = model.Login;
                        editedUser.UserFullName = model.UserName;
                        //if (!string.IsNullOrEmpty(model.ChiefId))
                        //{
                        //    int chiefId;
                        //    if (int.TryParse(model.ChiefId, out chiefId))
                        //    {
                        //        var chief = context.Users.FirstOrDefault(x => x.UserId == chiefId);
                        //        if (chief != null)
                        //        {
                        //            editedUser.RecipChief = chief;
                        //        }
                        //    }
                        //}
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool DeleteUserById(int id)
        {
            UserProfile user = null;
            try
            {
                using (var context = new TaskManagerContext())
                {
                    user = context.Users.FirstOrDefault(x => x.UserId == id);

                    if (user != null)
                    {
                        if (WebSecurity.CurrentUserName.Equals(user.UserName))
                        {
                            WebSecurity.Logout();
                        }
                        if (Roles.GetRolesForUser(user.UserName).Length > 0)
                        {
                            Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
                        }
                        ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.UserName);
                        //((SimpleMembershipProvider) Membership.Provider).DeleteUser(user.UserName, true);
                        user.IsActive = false;
                        context.SaveChanges();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return false;

        }

        public bool IsUserInAnyRole()
        {
            return IsAdmin() || IsChief() || IsMasterChief() || IsRecipient() || IsSender();
        }

        public int GetNewUsersCount()
        {
            int count = 0;
            foreach (UserProfile user in GetAllUsers())
            {
                if (Roles.GetRolesForUser(user.UserName).Length == 0 && user.IsActive)
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsNewUser(UserProfile user)
        {
            return !GetRolesForUser(user.UserName).Any();
        }

        public IEnumerable<UserProfile> GetChiefs()
        {
            return GetAllUsers().Where(user => Roles.IsUserInRole(user.UserName, "Chief"));
        }

        public IEnumerable<SelectListItem> GetPrioritiesSelectedList(string selectedPriorityId, TaskManagerContext context = null)
        {
            bool needToDispose = false;
            if (context == null)
            {
                context = new TaskManagerContext();
                needToDispose = true;
            }
            var priorities = context.Priorities.ToList();
            var priorList = new List<SelectListItem>();
            priorities.ForEach(x => priorList.Add(new SelectListItem
            {
                Text = x.PriorityName,
                Value = x.PriorityId.ToString(),
                Selected =
                    selectedPriorityId.Equals("0", StringComparison.InvariantCultureIgnoreCase)
                    ? x.PriorityName.Equals("Средний", StringComparison.InvariantCultureIgnoreCase)
                    : x.PriorityId.ToString().Equals(selectedPriorityId, StringComparison.InvariantCultureIgnoreCase)
            }));
            if (needToDispose)
            {
                context.Dispose();
            }
            return priorList;
        }

        public IEnumerable<SelectListItem> GetRecipientsSelectedList(string firstElementTitle, string selectedRecipientId, TaskManagerContext context = null)
        {
            bool needToDispose = false;
            if (context == null)
            {
                context = new TaskManagerContext();
                needToDispose = true;
            }
            var recipients = context.Users.ToList().Where(user => Roles.IsUserInRole(user.UserName, "Recipient")).ToList();
            var recipSelectList = new List<SelectListItem>();
            recipSelectList.Add(new SelectListItem
            {
                Text = firstElementTitle,
                Value = "0",
                Selected = selectedRecipientId == "0"
            });
            recipients.ForEach(x => recipSelectList.Add(new SelectListItem
            {
                Text = x.UserFullName,
                Value = x.UserId.ToString(),
                Selected = selectedRecipientId == x.UserId.ToString()
            }));
            if (needToDispose)
            {
                context.Dispose();
            }
            return recipSelectList;
        }

        public UserProfile CheckUser(string username)
        {
            using (var context = new TaskManagerContext())
            {
                return context.Users.FirstOrDefault(
                    x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public void SaveChages()
        {
            using (var context = new TaskManagerContext())
            {
                context.SaveChanges();
            }
        }
    }
}
