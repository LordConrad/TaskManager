using System.Linq;
using System.Web.Security;
using TaskManager.BusinessLogic.Models;
using TaskManager.Models;
using TaskManager.Models.Admin;
using WebMatrix.WebData;

namespace TaskManager.Converters
{
    public static class EntityConverter
    {

        public static RegisterModel Convert(RegisterViewModel model)
        {
            return new RegisterModel
            {
                UserName = model.UserName,
                UserFullName = model.UserFullName,
                Password = model.Password
            };

        }

        public static UserViewModel Convert(UserProfile user)
        {
            return new UserViewModel
            {
                UserFullName = user.UserFullName,
                UserId = user.UserId,
                IsActive = user.IsActive,
                UserName = user.UserName
            };
        }

        public static RegisterViewModel Convert(RegisterModel model)
        {
            return new RegisterViewModel
            {
                UserName = model.UserName,
                UserFullName = model.UserFullName,
                Password = model.Password
            };

        }

        public static TaskViewModel Convert(Task task)
        {
            return new TaskViewModel
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskId = task.TaskId,
                TaskPriority = task.TaskPriority,
                TaskText = task.TaskText
            };
        }
        public static Task Convert(TaskViewModel task)
        {
            return new Task
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskId = task.TaskId,
                TaskPriority = task.TaskPriority,
                TaskText = task.TaskText
            };
        }

        
    }
}
