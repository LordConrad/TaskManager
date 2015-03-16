using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Providers;

namespace TaskManager.BusinessLogic.Services
{
    public class TasksService
    {
        public bool AddTask(Task task)
        {
            return TasksProvider.AddTask(task);
        }

        public IEnumerable<Task> GetTasksBySender(int senderId)
        {
            return TasksProvider.GetTasksBySender(senderId);
        }

        public static Task GetTasksById(int taskId)
        {
            return TasksProvider.GetTasksById(taskId);
        }
    }
}
