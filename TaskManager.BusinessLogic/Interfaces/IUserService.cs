using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserProfileBL CurrentUser();
        bool IsAdmin();
        bool IsChief();
        bool IsRecipient();
        bool IsSender();
        bool IsMasterChief();
        bool IsNewUser(UserProfileBL user);
        IEnumerable<UserProfileBL> GetChiefs();
        List<UserProfileBL> GetAllUsers();
        IEnumerable<string> GetRolesForUser(string userName);
        UserProfileBL GetUserByLogin(string username);
        UserProfileBL GetUserById(int id);
        bool SaveEditedUser(UserModelBl model);
        bool DeleteUserById(int id);
        bool IsUserInAnyRole();
        int GetNewUsersCount();
        UserProfileBL CheckUser(string username);
    }
}
