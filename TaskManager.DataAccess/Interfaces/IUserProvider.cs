﻿using System.Collections.Generic;
using System.Web.Mvc;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Interfaces
{
    public interface IUserProvider
    {
        UserProfile GetCurrentUser();
        bool IsAdmin();
        bool IsChief();
        bool IsRecipient();
        bool IsSender();
        bool IsMasterChief();
        List<UserProfile> GetAllUsers();
        IEnumerable<UserProfile> GetAllRecipients();
        IEnumerable<string> GetRolesForUser(string userName);
        UserProfile GetUserByLogin(string login);
        UserProfile GetUserById(int id);
	    bool SaveEditedUser(UserProfile model, string[] newRoles);
        bool DeleteUserById(int id);
        bool IsUserInAnyRole();
        int GetNewUsersCount();
        bool IsNewUser(UserProfile user);
        IEnumerable<UserProfile> GetChiefs();
        IEnumerable<SelectListItem> GetPrioritiesSelectedList(string selectedPriorityId, TaskManagerContext context = null);
        IEnumerable<SelectListItem> GetRecipientsSelectedList(string firstElementTitle, string selectedRecipientId, TaskManagerContext context = null);
        UserProfile CheckUser(string username);
        string[] GetRolesNamesArray(string model);
        IEnumerable<string> GetRolesNames(); 
        void SaveChages();
    }
}
