using System.Linq;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Models;
using DAL = TaskManager.DataAccess.Models;
using BL = TaskManager.BusinessLogic.Models; 


namespace TaskManager.BusinessLogic.Converters
{
    public static class EntityConverter
    {
        public static BL.Task Convert(DAL.Task task)
        {
            return new BL.Task
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
                TaskEeventLogs = task.TaskEeventLogs.Select(Convert).ToList(),
                TaskId = task.TaskId,
                TaskPriority = Convert(task.TaskPriority),
                TaskRecipient = Convert(task.TaskRecipient),
                TaskSender = Convert(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static DAL.Task Convert(BL.Task task)
        {
            return new DataAccess.Models.Task
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
                TaskEeventLogs = task.TaskEeventLogs.Select(Convert).ToList(),
                TaskId = task.TaskId,
                TaskPriority = Convert(task.TaskPriority),
                TaskRecipient = Convert(task.TaskRecipient),
                TaskSender = Convert(task.TaskSender),
                TaskText = task.TaskText
            };
        }

        public static BL.Priority Convert(DAL.Priority priority)
        {
            return new BL.Priority
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(Convert).ToList()
            };
        }

        public static DAL.Priority Convert(BL.Priority priority)
        {
            return new DataAccess.Models.Priority
            {
                PriorityId = priority.PriorityId,
                PriorityName = priority.PriorityName,
                SamePriorityTasks = priority.SamePriorityTasks.Select(Convert).ToList()
            };
        }

        public static BL.Comment Convert(DAL.Comment comment)
        {
            return new BL.Comment
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

        public static DAL.Comment Convert(BL.Comment comment)
        {
            return new DataAccess.Models.Comment
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

        public static BL.UserProfile Convert(DAL.UserProfile userProfile)
        {
            return new BL.UserProfile
            {
                Comments = userProfile.Comments.Select(Convert).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(Convert).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(Convert).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(Convert).ToList(),
                UserFullName = userProfile.UserFullName
            };
        }

        public static DAL.UserProfile Convert(BL.UserProfile userProfile)
        {
            return new DAL.UserProfile
            {
                Comments = userProfile.Comments.Select(Convert).ToList(),
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                Logs = userProfile.Logs.Select(Convert).ToList(),
                RecipTasks = userProfile.RecipTasks.Select(Convert).ToList(),
                SendedTasks = userProfile.SendedTasks.Select(Convert).ToList(),
                UserFullName = userProfile.UserFullName,
            };
        }

        public static BL.TaskEventLog Convert(DAL.TaskEventLog taskEventLog)
        {
            return new BL.TaskEventLog
            {
                EventDateTime = taskEventLog.EventDateTime,
                NewValue = taskEventLog.NewValue,
                Task = Convert(taskEventLog.Task),
                UserId = taskEventLog.UserId,
                TaskId = taskEventLog.TaskId,
                OldValue = taskEventLog.OldValue,
                PropertyName = taskEventLog.PropertyName,
                TaskEventLogId = taskEventLog.TaskEventLogId,
                User = Convert(taskEventLog.User)
            };
        }

        public static DAL.TaskEventLog Convert(BL.TaskEventLog taskEeventLog)
        {
            return new DAL.TaskEventLog
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                Task = Convert(taskEeventLog.Task),
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
                User = Convert(taskEeventLog.User)
            };
        }
    }
}
