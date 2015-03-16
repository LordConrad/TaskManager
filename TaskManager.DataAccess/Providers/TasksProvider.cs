using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMatrix.WebData;
using Task = TaskManager.DataAccess.Task;

namespace TaskManager.DataAccess.Providers
{
    public static class TasksProvider
    {

        public const string DateTimeFormatFull = "dd.MM.yy  HH:mm";
        public static bool AddTask(Task task)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var newTask = context.Tasks.Add(task);
                    if (newTask.TaskEeventLogs == null) newTask.TaskEeventLogs = new List<TaskEeventLog>();
                    newTask.TaskEeventLogs.Add(new TaskEeventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "CreateDate",
                        UserId = WebSecurity.CurrentUserId,
                        NewValue = newTask.CreateDate.ToString(DateTimeFormatFull),
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

        public static IEnumerable<Task> GetTasksBySender(int senderId)
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

        public static Task GetTasksById(int taskId)
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
    }
}
