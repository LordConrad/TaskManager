using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        UserProfile GetCurrentUser();
        bool IsAdmin();
        bool IsChief();
        bool IsRecipient();
        bool IsSender();
        bool IsMasterChief();
        bool IsNewUser(UserProfile user);
        IEnumerable<UserProfile> GetAllChiefs();
        IEnumerable<UserProfile> GetAllRecipients();
        List<UserProfile> GetAllUsers();
        IEnumerable<string> GetRolesForUser(string userName);
        UserProfile GetUserByLogin(string username);
        UserProfile GetUserById(int id);
        bool SaveEditedUser(UserProfile model);
        bool DeleteUserById(int id);
        bool IsUserInAnyRole();
        int GetNewUsersCount();
        UserProfile CheckUser(string username);
        IEnumerable<string> GetRolesNames();
    }
}
