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
        private readonly IUserProvider _userProvider;

        public UserService(UserProvider up)
        {
            _userProvider = up;
        }

        public UserProfile GetCurrentUser()
        {
            return EntityConverter.Convert(_userProvider.GetCurrentUser());
        }

        public bool IsAdmin()
        {
            return _userProvider.IsAdmin();
        }

        public bool IsChief()
        {
            return _userProvider.IsChief();
        }

        public bool IsRecipient()
        {
            return _userProvider.IsRecipient();
        }

        public bool IsSender()
        {
            return _userProvider.IsSender();
        }

        public bool IsMasterChief()
        {
            return _userProvider.IsMasterChief();
        }

        public bool IsNewUser(UserProfile user)
        {
            return _userProvider.IsNewUser(EntityConverter.Convert(user));
        }

        public IEnumerable<UserProfile> GetAllChiefs()
        {
            return _userProvider.GetChiefs().Select(EntityConverter.Convert);
        }

        public List<UserProfile> GetAllUsers()
        {
            return _userProvider.GetAllUsers().Select(EntityConverter.Convert).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return _userProvider.GetRolesForUser(userName);
        }

        public UserProfile GetUserByLogin(string username)
        {
            return EntityConverter.Convert(_userProvider.GetUserByLogin(username));
        }

        public UserProfile GetUserById(int id)
        {
            return EntityConverter.Convert(_userProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserProfile model)
        {
			return _userProvider.SaveEditedUser(EntityConverter.Convert(model), _userProvider.GetRolesNamesArray(model.UserFullName));
        }

        public bool DeleteUserById(int id)
        {
            return _userProvider.DeleteUserById(id);
        }

        public bool IsUserInAnyRole()
        {
            return _userProvider.IsUserInAnyRole();
        }

        public int GetNewUsersCount()
        {
            return _userProvider.GetNewUsersCount();
        }

        public UserProfile CheckUser(string username)
        {
            return EntityConverter.Convert(_userProvider.CheckUser(username));
        }

        public IEnumerable<UserProfile> GetAllRecipients()
        {
            return _userProvider.GetAllRecipients().Select(EntityConverter.Convert);
        }

        public IEnumerable<string> GetRolesNames()
        {
            return _userProvider.GetRolesNames();
        }

		
    }
}
