using System.Linq;
using TaskManager.BusinessLogic.Models;
using TaskManager.Models;

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
                Comments = task.Comments.Select(Convert).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(Convet).ToList(),
                TaskId = task.TaskId,
                TaskPriority = task.TaskPriority,
                TaskRecipient = ConvertToUserModelBl(task.TaskRecipient),
                TaskSender = task.TaskSender,
                TaskText = task.TaskText
            };
        }
        public static Task Convert(TaskViewModel task)
        {
            return new Task
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments,
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs,
                TaskId = task.TaskId,
                TaskPriority = task.TaskPriority,
                TaskRecipient = task.TaskRecipient,
                TaskSender = task.TaskSender,
                TaskText = task.TaskText
            };
        }

        public static TaskEventLogViewModel Convet(TaskEventLog taskEventLog)
        {
            return new TaskEventLogViewModel
            {
                EventDateTime = taskEventLog.EventDateTime,
                NewValue = taskEventLog.NewValue,
                Task = taskEventLog.Task,
                UserId = taskEventLog.UserId,
                TaskId = taskEventLog.TaskId,
                OldValue = taskEventLog.OldValue,
                PropertyName = taskEventLog.PropertyName,
                TaskEventLogId = taskEventLog.TaskEventLogId,
                User = taskEventLog.User
            };
        }

        public static TaskEventLog Convet(TaskEventLogViewModel taskEventLog)
        {
            return new TaskEventLog
            {
                EventDateTime = taskEventLog.EventDateTime,
                NewValue = taskEventLog.NewValue,
                Task = Convert(taskEventLog.Task),
                UserId = taskEventLog.UserId,
                TaskId = taskEventLog.TaskId,
                OldValue = taskEventLog.OldValue,
                PropertyName = taskEventLog.PropertyName,
                TaskEventLogId = taskEventLog.TaskEventLogId,
                User = taskEventLog.User
            };
        }

        public static Comment Convert(CommentViewModel comment)
        {
            return new Comment
            {
                Author = Convert(comment.Author),
                Task = Convert(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static CommentViewModel Convert(Comment comment)
        {
            return new CommentViewModel
            {
                Author = Convert(comment.Author),
                Task = Convert(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static UserViewModel Convert(UserProfile userModel)
        {
            return new UserViewModel
            {
                UserId = userModel.UserId,
                UserName = userModel.UserName,
                ChiefId = userModel.ChiefId,
                IsAdmin = userModel.IsAdmin,
                IsChief = userModel.IsChief,
                IsMasterChief = userModel.IsMasterChief,
                IsRecipient = userModel.IsMasterChief,
                IsSender = userModel.IsSender,
                Login = userModel.Login
            };
        }

        public static UserProfile ConvertToUserModelBl(UserViewModel userModelBl)
        {
            return new UserProfile
            {
                UserId = userModelBl.UserId,
                UserName = userModelBl.UserName,
                ChiefId = userModelBl.ChiefId,
                IsAdmin = userModelBl.IsAdmin,
                IsChief = userModelBl.IsChief,
                IsMasterChief = userModelBl.IsMasterChief,
                IsRecipient = userModelBl.IsMasterChief,
                IsSender = userModelBl.IsSender,
                Login = userModelBl.Login
            };
        }
    }
}
