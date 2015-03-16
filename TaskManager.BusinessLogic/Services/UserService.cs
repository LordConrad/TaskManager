using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Providers;

namespace TaskManager.BusinessLogic.Services
{
    public class UserService
    {

        public UserProfileBL CurrentUser()
        {
            return ConvertUserProfileToBl(UserProvider.CurrentUser);
        }

        public bool IsAdmin()
        {
            return UserProvider.IsAdmin;
        }

        public bool IsChief()
        {
            return UserProvider.IsChief;
        }

        public bool IsRecipient()
        {
            return UserProvider.IsRecipient;
        }

        public bool IsSender()
        {
            return UserProvider.IsSender;
        }

        public bool IsMasterChief()
        {
            return UserProvider.IsMasterChief;
        }

        public bool IsNewUser(UserProfileBL user)
        {
            return UserProvider.IsNewUser(user);
        }

        public IEnumerable<UserProfileBL> GetChiefs()
        {
            return UserProvider.GetChiefs().Select(ConvertUserProfileToBl);
        }

        public List<UserProfileBL> GetAllUsers()
        {
            return UserProvider.GetAllUsers().Select(ConvertUserProfileToBl).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return UserProvider.GetRolesForUser(userName);
        }

        public UserProfileBL GetUserByLogin(string username)
        {
            return ConvertUserProfileToBl(UserProvider.GetUserByLogin(username));
        }

        public UserProfileBL GetUserById(int id)
        {
            return ConvertUserProfileToBl(UserProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserModelBl model)
        {
            return UserProvider.SaveEditedUser(ConvertUserModelToDal(model));
        }

        public bool DeleteUserById(int id)
        {
            return UserProvider.DeleteUserById(id);
        }

        public bool IsUserInAnyRole()
        {
            return UserProvider.IsUserInAnyRole;
        }

        public int GetNewUsersCount()
        {
            return UserProvider.GetNewUsersCount();
        }

        private UserModel ConvertUserModelToDal(UserModelBl userModelFromDal)
        {
            return new UserModel
            {
                UserId = userModelFromDal.UserId,
                UserName = userModelFromDal.UserName,
                Login = userModelFromDal.Login,
                ChiefId = userModelFromDal.ChiefId,
                IsAdmin = userModelFromDal.IsAdmin,
                IsChief = userModelFromDal.IsChief,
                IsMasterChief = userModelFromDal.IsMasterChief,
                IsRecipient = userModelFromDal.IsRecipient,
                IsSender = userModelFromDal.IsSender,
            };

        }

        private UserProfileBL ConvertUserProfileToBl(UserProfile profileFromDal)
        {
            return new UserProfileBL
            {
                UserId = profileFromDal.UserId,
                UserName = profileFromDal.UserName,
                UserFullName = profileFromDal.UserFullName,
                Comments = profileFromDal.Comments,
                IsActive = profileFromDal.IsActive,
                Logs = profileFromDal.Logs,
                RecipTasks = profileFromDal.RecipTasks,
                SendedTasks = profileFromDal.SendedTasks,
            };

        }
    }
}
