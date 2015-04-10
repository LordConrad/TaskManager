using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using TaskManager.DataAccess.Interfaces;
using TaskManager.DataAccess.Models;
using WebMatrix.WebData;

namespace TaskManager.DataAccess.Providers
{
    public class TasksProvider : ITasksProvider
    {

        public bool AddTask(Task task)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var newTask = context.Tasks.Add(task);
                    if (newTask.TaskEeventLogs == null) newTask.TaskEeventLogs = new List<TaskEventLog>();
                    newTask.TaskEeventLogs.Add(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "CreateDate",
                        UserId = WebSecurity.CurrentUserId,
                        NewValue = newTask.CreateDate.ToString("dd.MM.yy  HH:mm"),
                    });
                    context.SaveChanges();
                }
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Task> GetTasksBySender(int senderId)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    return context.Tasks.Where(x => x.SenderId == senderId)
                        .Include(x => x.TaskRecipient)
                        .Include(x => x.TaskSender)
                        .ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task GetTasksById(int taskId)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    return context.Tasks
                        .Include(x => x.Comments)
                        .Include(x => x.TaskPriority)
                        .Include(x => x.TaskRecipient)
                        .Include(x => x.TaskSender)
                        .FirstOrDefault(x => x.TaskId == taskId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Task> GetTasks()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks.Where(x => x.AcceptCpmpleteDate.HasValue)
                    .Include(x => x.TaskRecipient)
                    .ToList();
            }
        }

        public IList<Task> GetTasksForCurrrentUser()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks.Where(x => x.RecipientId == WebSecurity.CurrentUserId && !x.AcceptCpmpleteDate.HasValue)
                    .Include(x => x.Comments)
                    .Include(x => x.TaskPriority)
                    .Include(x => x.TaskSender)
                    .ToList();
            }
        } 

        public void UpdateTaskText(Task model)
        {
            using (var context = new TaskManagerContext())
            {
                var task = context.Tasks.FirstOrDefault(x => x.TaskId == model.TaskId);
                if (task != null && !task.TaskText.Trim().Equals(model.TaskText.Trim(), StringComparison.InvariantCultureIgnoreCase))
                {
                    task.TaskEeventLogs.Add(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "TaskText",
                        UserId = WebSecurity.CurrentUserId,
                        OldValue = task.TaskText,
                        NewValue = model.TaskText
                    });
                    task.TaskText = model.TaskText;

                    context.SaveChanges();
                }
            }
        }

        public void DeleteTask(int taskId)
        {
            using (var context = new TaskManagerContext())
            {
                var task = context.Tasks.FirstOrDefault(x => x.TaskId == taskId);

                if (task != null)
                {
                    context.Tasks.Remove(task);
                    context.SaveChanges();
                }
            }
        }

        public void ConfirmTask(int id)
        {
            using (var context = new TaskManagerContext())
            {
                var task = context.Tasks.FirstOrDefault(x => x.TaskId == id);
                if (task != null)
                {
                    task.TaskEeventLogs.Add(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "AcceptCpmpleteDate",
                        UserId = WebSecurity.CurrentUserId,
                        OldValue = task.AcceptCpmpleteDate.ToString(),
                        NewValue = DateTime.Now.ToString()
                    });
                    task.AcceptCpmpleteDate = DateTime.Now;
                }
                context.SaveChanges();
            }
        }

        public int SenderCompleteTasksCount()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks.Count(x => x.SenderId == WebSecurity.CurrentUserId && x.CompleteDate.HasValue && !x.AcceptCpmpleteDate.HasValue);
            }
        }

        public int GetNotAssignedTasksCount()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Tasks.Count(x => !x.RecipientId.HasValue);
            }
        }

        public IEnumerable<Priority> GetPriorityList()
        {
            using (var context = new TaskManagerContext())
            {
                return context.Priorities;
            }
        }

        public bool UpdateTask(Task task)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    context.Entry(task).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public IEnumerable<Comment> GetCommentsForTask(int taskId)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    return context.Comments.Where(x => x.TaskId == taskId);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
