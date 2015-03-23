using System.Linq;
using TaskManager.BusinessLogic.Models;
using TaskManager.Models;

namespace TaskManager.Converters
{
    public static class EntityConverter
    {

        public static RegisterModelBl ConverttoRegisterModelBl(RegisterViewModel model)
        {
            return new RegisterModelBl
            {
                UserName = model.UserName,
                UserFullName = model.UserFullName,
                Password = model.Password
            };

        }

        public static RegisterViewModel ConverttoRegisterModelUi(RegisterModelBl model)
        {
            return new RegisterViewModel
            {
                UserName = model.UserName,
                UserFullName = model.UserFullName,
                Password = model.Password
            };

        }

        public static TaskViewModel ConverttoTaskUi(TaskBL task)
        {
            return new TaskViewModel
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments.Select(ConverttoCommentUi).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(ConvettoTaskEventLogUi).ToList(),
                TaskId = task.TaskId,
                TaskPriority = task.TaskPriority,
                TaskRecipient = ConverttoUserModelBl(task.TaskRecipient),
                TaskSender = task.TaskSender,
                TaskText = task.TaskText
            };
        }
        public static TaskBL ConverttoTaskBl(TaskViewModel task)
        {
            return new TaskBL
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

        public static TaskEventLogViewModel ConvettoTaskEventLogUi (TaskEventLogBl taskEventLog)
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

        public static TaskEventLogBl ConvettoTaskEventLogUi(TaskEventLogViewModel taskEventLog)
        {
            return new TaskEventLogBl
            {
                EventDateTime = taskEventLog.EventDateTime,
                NewValue = taskEventLog.NewValue,
                Task = ConverttoTaskBl(taskEventLog.Task),
                UserId = taskEventLog.UserId,
                TaskId = taskEventLog.TaskId,
                OldValue = taskEventLog.OldValue,
                PropertyName = taskEventLog.PropertyName,
                TaskEventLogId = taskEventLog.TaskEventLogId,
                User = taskEventLog.User
            };
        }

        public static CommentBL ConverttoCommentBl(CommentViewModel comment)
        {
            return new CommentBL
            {
                Author = ConverttoUserProfileDal(comment.Author),
                Task = ConverttoTaskDal(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static CommentViewModel ConverttoCommentUi(CommentBL comment)
        {
            return new CommentViewModel
            {
                Author = Convert(comment.Author),
                Task = ConverttoTaskDal(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static UserViewModel ConverttoUserModelUi(UserModelBl userModelBl)
        {
            return new UserViewModel
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

        public static UserModelBl ConverttoUserModelBl(UserViewModel userModelBl)
        {
            return new UserModelBl
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
