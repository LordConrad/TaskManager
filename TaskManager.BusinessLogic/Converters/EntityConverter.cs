using System.Linq;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Models;


namespace TaskManager.BusinessLogic.Converters
{
    public static class EntityConverter
    {
        public static TaskBl ConvertToTaskBl(Task task)
        {
            return new TaskBl
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments.Select(ConvertToCommentBl).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(ConvertToTaskEventLogBl).ToList(),
                TaskId = task.TaskId,
                TaskPriority = ConvertoPriorityBl(task.TaskPriority),
                TaskRecipient = ConvertToUserProfileBl(task.TaskRecipient),
                TaskSender = ConvertToUserProfileBl(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static Task ConvertToTaskDal(TaskBl task)
        {
            return new Task
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                Comments = task.Comments.Select(ConvertToCommentDal).ToList(),
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = task.PriorityId,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskEeventLogs = task.TaskEeventLogs.Select(ConvertToTaskEventLogDal).ToList(),
                TaskId = task.TaskId,
                TaskPriority = ConvertoPriorityDal(task.TaskPriority),
                TaskRecipient = ConvertToUserProfileDal(task.TaskRecipient),
                TaskSender = ConvertToUserProfileDal(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static PriorityBl ConvertoPriorityBl(Priority priority)
        {
            return new PriorityBl
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(ConvertToTaskBl).ToList()
            };
        }

        public static Priority ConvertoPriorityDal(PriorityBl priority)
        {
            return new Priority
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(ConvertToTaskDal).ToList()
            };
        }

        public static CommentBl ConvertToCommentBl(Comment comment)
        {
            return new CommentBl
            {
                Author = ConvertToUserModelBl(comment.Author),
                Task = ConvertToTaskBl(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static Comment ConvertToCommentDal(CommentBl comment)
        {
            return new Comment
            {
                Author = ConvertToUserProfileDal(comment.Author),
                Task = ConvertToTaskDal(comment.Task),
                TaskId = comment.TaskId,
                AuthorId = comment.AuthorId,
                CommentDate = comment.CommentDate,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };
        }

        public static UserProfileBl ConvertToUserProfileBl(UserProfile userProfile)
        {
            return new UserProfileBl
            {
                Comments = userProfile.Comments.Select(ConvertToCommentBl).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(ConvertToTaskEventLogBl).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(ConvertToTaskBl).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(ConvertToTaskBl).ToList(),
                UserFullName = userProfile.UserFullName
            };
        }

        public static UserProfile ConvertToUserProfileDal(UserProfileBl userProfile)
        {
            return new UserProfile
            {
                Comments = userProfile.Comments.Select(ConvertToCommentDal).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(ConvertToTaskEventLogDal).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(ConvertToTaskDal).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(ConvertToTaskDal).ToList(),
                UserFullName = userProfile.UserFullName,
            };
        }

        public static TaskEventLogBl ConvertToTaskEventLogBl(TaskEeventLog taskEeventLog)
        {
            return new TaskEventLogBl
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                Task = ConvertToTaskBl(taskEeventLog.Task),
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
                User = ConvertToUserProfileBl(taskEeventLog.User)
            };
        }

        public static TaskEeventLog ConvertToTaskEventLogDal(TaskEventLogBl taskEeventLog)
        {
            return new TaskEeventLog
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                Task = ConvertToTaskDal(taskEeventLog.Task),
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
                User = ConvertToUserProfileDal(taskEeventLog.User)
            };
        }

        public static UserModel ConvertToUserModelDal(UserModelBl userModelFromDal)
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

        public static UserModelBl ConvertToUserModelBl(UserModel userModelFromDal)
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
