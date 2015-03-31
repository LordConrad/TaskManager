using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserProfileBl GetCurrentUser();
        bool IsAdmin();
        bool IsChief();
        bool IsRecipient();
        bool IsSender();
        bool IsMasterChief();
        bool IsNewUser(UserProfileBl user);
        IEnumerable<UserProfileBl> GetChiefs();
        List<UserProfileBl> GetAllUsers();
        IEnumerable<string> GetRolesForUser(string userName);
        UserProfileBl GetUserByLogin(string username);
        UserProfileBl GetUserById(int id);
        bool SaveEditedUser(UserModelBl model);
        bool DeleteUserById(int id);
        bool IsUserInAnyRole();
        int GetNewUsersCount();
        UserProfileBl CheckUser(string username);
    }
}
