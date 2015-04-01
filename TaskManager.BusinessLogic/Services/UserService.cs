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
        private readonly IUserProvider userProvider;

        public UserService(UserProvider up)
        {
            userProvider = up;
        }

        public UserProfileBl GetCurrentUser()
        {
            return EntityConverter.ConvertToUserProfileBl(userProvider.GetCurrentUser());
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

        public bool IsNewUser(UserProfileBl user)
        {
            return userProvider.IsNewUser(EntityConverter.ConvertToUserProfileDal(user));
        }

        public IEnumerable<UserProfileBl> GetChiefs()
        {
            return userProvider.GetChiefs().Select(EntityConverter.ConvertToUserProfileBl);
        }

        public List<UserProfileBl> GetAllUsers()
        {
            return userProvider.GetAllUsers().Select(EntityConverter.ConvertToUserProfileBl).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return userProvider.GetRolesForUser(userName);
        }

        public UserProfileBl GetUserByLogin(string username)
        {
            return EntityConverter.ConvertToUserProfileBl(userProvider.GetUserByLogin(username));
        }

        public UserProfileBl GetUserById(int id)
        {
            return EntityConverter.ConvertToUserProfileBl(userProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserModelBl model)
        {
            return userProvider.SaveEditedUser(EntityConverter.ConvertToUserModelDal(model));
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

        public UserProfileBl CheckUser(string username)
        {
            return EntityConverter.ConvertToUserProfileBl(userProvider.CheckUser(username));
        }

    }
}
