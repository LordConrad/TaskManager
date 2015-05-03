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
        private IUserProvider _userProvider;

        private IUserProvider UserProvider
        {
            get
            {
                if (_userProvider == null)
                {
                    _userProvider = new UserProvider();
                }
                return _userProvider;
            }
        }
        
        public UserProfile GetCurrentUser()
        {
            return EntityConverter.Convert(UserProvider.GetCurrentUser());
        }

        public bool IsAdmin()
        {
            return UserProvider.IsAdmin();
        }

        public bool IsChief()
        {
            return UserProvider.IsChief();
        }

        public bool IsRecipient()
        {
            return UserProvider.IsRecipient();
        }

        public bool IsSender()
        {
            return UserProvider.IsSender();
        }

        public bool IsMasterChief()
        {
            return UserProvider.IsMasterChief();
        }

        public bool IsNewUser(UserProfile user)
        {
            return UserProvider.IsNewUser(EntityConverter.Convert(user));
        }

        public IEnumerable<UserProfile> GetAllChiefs()
        {
            return UserProvider.GetChiefs().Select(EntityConverter.Convert);
        }

        public List<UserProfile> GetAllUsers()
        {
            return UserProvider.GetAllUsers().Select(EntityConverter.Convert).ToList();
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return UserProvider.GetRolesForUser(userName);
        }

        public UserProfile GetUserByLogin(string username)
        {
            return EntityConverter.Convert(UserProvider.GetUserByLogin(username));
        }

        public UserProfile GetUserById(int id)
        {
            return EntityConverter.Convert(UserProvider.GetUserById(id));
        }

        public bool SaveEditedUser(UserProfile model)
        {
			return UserProvider.SaveEditedUser(EntityConverter.Convert(model), UserProvider.GetRolesNamesArray(model.UserFullName));
        }

        public bool DeleteUserById(int id)
        {
            return UserProvider.DeleteUserById(id);
        }

        public bool IsUserInAnyRole()
        {
            return UserProvider.IsUserInAnyRole();
        }

        public int GetNewUsersCount()
        {
            return UserProvider.GetNewUsersCount();
        }

        public UserProfile CheckUser(string username)
        {
            return EntityConverter.Convert(UserProvider.CheckUser(username));
        }

        public IEnumerable<UserProfile> GetAllRecipients()
        {
            return UserProvider.GetAllRecipients().Select(EntityConverter.Convert);
            
        }

        public IEnumerable<string> GetRolesNames()
        {
            return UserProvider.GetRolesNames();
        }

		
    }
}
