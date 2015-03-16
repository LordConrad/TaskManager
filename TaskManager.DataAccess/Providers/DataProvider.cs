using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace TaskManager.DataAccess.Providers
{
    public static class DataProvider
    {

        public const string DateFormat = "dd.MM.yy";
        

        public static IEnumerable<SelectListItem> GetPrioritiesSelectedList(string selectedPriorityId, TaskManagerContext context = null)
        {
            bool needToDispose = false;
            if (context == null)
            {
                context = new TaskManagerContext();
                needToDispose = true;
            }
            var priorities = context.Priorities.ToList();
            var priorList = new List<SelectListItem>();
            priorities.ForEach(x => priorList.Add(new SelectListItem
            {
                Text = x.PriorityName,
                Value = x.PriorityId.ToString(),
                Selected =
                    selectedPriorityId.Equals("0", StringComparison.InvariantCultureIgnoreCase)
                    ? x.PriorityName.Equals("Средний", StringComparison.InvariantCultureIgnoreCase)
                    : x.PriorityId.ToString().Equals(selectedPriorityId, StringComparison.InvariantCultureIgnoreCase)
            }));
            if (needToDispose)
            {
                context.Dispose();
            }
            return priorList;
        }

        public static IEnumerable<SelectListItem> GetRecipientsSelectedList(string firstElementTitle, string selectedRecipientId, TaskManagerContext context = null)
        {
            bool needToDispose = false;
            if (context == null)
            {
                context = new TaskManagerContext();
                needToDispose = true;
            }
            var recipients = context.Users.ToList().Where(user => Roles.IsUserInRole(user.UserName, "Recipient")).ToList();
            var recipSelectList = new List<SelectListItem>();
            recipSelectList.Add(new SelectListItem
            {
                Text = firstElementTitle,
                Value = "0",
                Selected = selectedRecipientId == "0"
            });
            recipients.ForEach(x => recipSelectList.Add(new SelectListItem
            {
                Text = x.UserFullName,
                Value = x.UserId.ToString(),
                Selected = selectedRecipientId == x.UserId.ToString()
            }));
            if (needToDispose)
            {
                context.Dispose();
            }
            return recipSelectList;
        }

        
    }


}