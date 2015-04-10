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

        public UserProfile GetCurrentUser()
        {
            return EntityConverter.Convert(userProvider.GetCurrentUser());
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

        public bool IsNewUser(UserProfile user)
        {
            return userProvider.IsNewUser(EntityConverter.Convert(user));
        }

        public IEnumerable<UserProfile> GetAllChiefs()
        {
            return userProvider.GetChiefs().Select(EntityConverter.Convert);
        }

        public List<UserProfile> GetAllUsers()
        {
            return userProvider.GetAllUsers().Select(EntityConverter.Convert).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return userProvider.GetRolesForUser(userName);
        }

        public UserProfile GetUserByLogin(string username)
        {
            return EntityConverter.Convert(userProvider.GetUserByLogin(username));
        }

        public UserProfile GetUserById(int id)
        {
            return EntityConverter.Convert(userProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserProfile model)
        {
			return userProvider.SaveEditedUser(EntityConverter.Convert(model), userProvider.GetRolesNamesArray(model.UserFullName));
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

        public UserProfile CheckUser(string username)
        {
            return EntityConverter.Convert(userProvider.CheckUser(username));
        }

        public IEnumerable<UserProfile> GetAllRecipients()
        {
            return userProvider.GetAllRecipients().Select(EntityConverter.Convert);
        }

		
    }
}
