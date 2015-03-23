using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.BusinessLogic.Converters;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Interfaces;
using TaskManager.DataAccess.Providers;

namespace TaskManager.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IUserProvider userProvider;

        public UserService(UserProvider up)
        {
            userProvider = up;
        }

        public UserProfileBL CurrentUser()
        {
            return EntityConverter.ConverttoUserProfileBl(userProvider.CurrentUser);
        }

        public bool IsAdmin()
        {
            return userProvider.IsAdmin();
        }

        public bool IsChief()
        {
            return userProvider.IsChief();
        }

        public bool IsRecipient()
        {
            return userProvider.IsRecipient();
        }

        public bool IsSender()
        {
            return userProvider.IsSender();
        }

        public bool IsMasterChief()
        {
            return userProvider.IsMasterChief();
        }

        public bool IsNewUser(UserProfileBL user)
        {
            return userProvider.IsNewUser(EntityConverter.ConverttoUserProfileDal(user));
        }

        public IEnumerable<UserProfileBL> GetChiefs()
        {
            return userProvider.GetChiefs().Select(EntityConverter.ConverttoUserProfileBl);
        }

        public List<UserProfileBL> GetAllUsers()
        {
            return userProvider.GetAllUsers().Select(EntityConverter.ConverttoUserProfileBl).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return userProvider.GetRolesForUser(userName);
        }

        public UserProfileBL GetUserByLogin(string username)
        {
            return EntityConverter.ConverttoUserProfileBl(userProvider.GetUserByLogin(username));
        }

        public UserProfileBL GetUserById(int id)
        {
            return EntityConverter.ConverttoUserProfileBl(userProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserModelBl model)
        {
            return userProvider.SaveEditedUser(EntityConverter.ConverttoUserModelDal(model));
        }

        public bool DeleteUserById(int id)
        {
            return userProvider.DeleteUserById(id);
        }

        public bool IsUserInAnyRole()
        {
            return userProvider.IsUserInAnyRole();
        }

        public int GetNewUsersCount()
        {
            return userProvider.GetNewUsersCount();
        }

        public UserProfileBL CheckUser(string username)
        {
            return EntityConverter.ConverttoUserProfileBl(userProvider.CheckUser(username));
        }

    }
}
