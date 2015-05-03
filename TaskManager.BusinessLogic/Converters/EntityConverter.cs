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
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskId = task.TaskId,
                Priority = (BL.Priority)task.TaskId,
                TaskText = task.TaskText
            };
        }

        public static DAL.Task Convert(BL.Task task)
        {
            return new DataAccess.Models.Task
            {
                AcceptCpmpleteDate = task.AcceptCpmpleteDate,
                AssignDateTime = task.AssignDateTime,
                CompleteDate = task.CompleteDate,
                CreateDate = task.CreateDate,
                Deadline = task.Deadline,
                IsRecipientViewed = task.IsRecipientViewed,
                PriorityId = (int)task.Priority,
                RecipientId = task.RecipientId,
                ResultComment = task.ResultComment,
                SenderId = task.SenderId,
                TaskId = task.TaskId,
                TaskText = task.TaskText
            };
        }

        public static BL.Comment Convert(DAL.Comment comment)
        {
            return new BL.Comment
            {
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
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                UserFullName = userProfile.UserFullName
            };
        }

        public static DAL.UserProfile Convert(BL.UserProfile userProfile)
        {
            return new DAL.UserProfile
            {
                UserName = userProfile.UserName,
                UserId = userProfile.UserId,
                IsActive = userProfile.IsActive,
                UserFullName = userProfile.UserFullName,
            };
        }

        public static BL.TaskEventLog Convert(DAL.TaskEventLog taskEventLog)
        {
            return new BL.TaskEventLog
            {
                EventDateTime = taskEventLog.EventDateTime,
                NewValue = taskEventLog.NewValue,
                UserId = taskEventLog.UserId,
                TaskId = taskEventLog.TaskId,
                OldValue = taskEventLog.OldValue,
                PropertyName = taskEventLog.PropertyName,
                TaskEventLogId = taskEventLog.TaskEventLogId,
            };
        }

        public static DAL.TaskEventLog Convert(BL.TaskEventLog taskEeventLog)
        {
            return new DAL.TaskEventLog
            {
                EventDateTime = taskEeventLog.EventDateTime,
                NewValue = taskEeventLog.NewValue,
                UserId = taskEeventLog.UserId,
                TaskId = taskEeventLog.TaskId,
                OldValue = taskEeventLog.OldValue,
                PropertyName = taskEeventLog.PropertyName,
                TaskEventLogId = taskEeventLog.TaskEventLogId,
            };
        }
    }
}
