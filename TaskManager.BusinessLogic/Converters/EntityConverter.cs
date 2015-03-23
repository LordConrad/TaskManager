using System.Linq;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.DataAccess.Converters
{
    public static class EntityConverter
    {
        public static TaskBL ConverttoTaskBl(Task task)
        {
            return new TaskBL
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments.Select(ConverttoCommentBl).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(ConverttoTaskEventLogBl).ToList(),
                TaskId = task.TaskId,
                TaskPriority = ConvertoPriorityBl(task.TaskPriority),
                TaskRecipient = ConverttoUserProfileBl(task.TaskRecipient),
                TaskSender = ConverttoUserProfileBl(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static Task ConverttoTaskDal(TaskBL task)
        {
            return new Task
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments.Select(ConverttoCommentDal).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(ConverttoTaskEventLogDal).ToList(),
                TaskId = task.TaskId,
                TaskPriority = ConvertoPriorityDal(task.TaskPriority),
                TaskRecipient = ConverttoUserProfileDal(task.TaskRecipient),
                TaskSender = ConverttoUserProfileDal(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static PriorityBL ConvertoPriorityBl(Priority priority)
        {
            return new PriorityBL
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(ConverttoTaskBl).ToList()
            };
        }

        public static Priority ConvertoPriorityDal(PriorityBL priority)
        {
            return new Priority
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(ConverttoTaskDal).ToList()
            };
        }

        public static CommentBL ConverttoCommentBl(Comment comment)
        {
            return new CommentBL
            {
                Author = ConverttoUserModelBl(comment.Author),
                Task = ConverttoTaskBl(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static Comment ConverttoCommentDal(CommentBL comment)
        {
            return new Comment
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

        public static UserProfileBL ConverttoUserProfileBl(UserProfile userProfile)
        {
            return new UserProfileBL
            {
                Comments = userProfile.Comments.Select(ConverttoCommentBl).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(ConverttoTaskEventLogBl).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(ConverttoTaskBl).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(ConverttoTaskBl).ToList(),
                UserFullName = userProfile.UserFullName
            };
        }

        public static UserProfile ConverttoUserProfileDal(UserProfileBL userProfile)
        {
            return new UserProfile
            {
                Comments = userProfile.Comments.Select(ConverttoCommentDal).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(ConverttoTaskEventLogDal).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(ConverttoTaskDal).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(ConverttoTaskDal).ToList(),
                UserFullName = userProfile.UserFullName,
            };
        }

        public static TaskEventLogBl ConverttoTaskEventLogBl(TaskEeventLog taskEeventLog)
        {
            return new TaskEventLogBl
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                Task = ConverttoTaskBl(taskEeventLog.Task),
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
                User = ConverttoUserProfileBl(taskEeventLog.User)
            };
        }

        public static TaskEeventLog ConverttoTaskEventLogDal(TaskEventLogBl taskEeventLog)
        {
            return new TaskEeventLog
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                Task = ConverttoTaskDal(taskEeventLog.Task),
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
                User = ConverttoUserProfileDal(taskEeventLog.User)
            };
        }

        public static UserModel ConverttoUserModelDal(UserModelBl userModelFromDal)
        {
            return new UserModel
            {
                UserId = userModelFromDal.UserId,
                UserName = userModelFromDal.UserName,
                Login = userModelFromDal.Login,
                ChiefId = userModelFromDal.ChiefId,
                IsAdmin = userModelFromDal.IsAdmin,
                IsChief = userModelFromDal.IsChief,
                IsMasterChief = userModelFromDal.IsMasterChief,
                IsRecipient = userModelFromDal.IsRecipient,
                IsSender = userModelFromDal.IsSender,
            };

        }

        public static UserModelBl ConverttoUserModelBl(UserModel userModelFromDal)
        {
            return new UserModelBl
            {
                UserId = userModelFromDal.UserId,
                UserName = userModelFromDal.UserName,
                Login = userModelFromDal.Login,
                ChiefId = userModelFromDal.ChiefId,
                IsAdmin = userModelFromDal.IsAdmin,
                IsChief = userModelFromDal.IsChief,
                IsMasterChief = userModelFromDal.IsMasterChief,
                IsRecipient = userModelFromDal.IsRecipient,
                IsSender = userModelFromDal.IsSender,
            };

        }
    }
}
