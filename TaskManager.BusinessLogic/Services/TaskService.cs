using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.BusinessLogic.Converters;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Interfaces;
using TaskManager.DataAccess.Providers;

namespace TaskManager.BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private ITaskProvider _taskProvider;

        private ITaskProvider TaskProvider
        {
            get
            {
                if (_taskProvider == null)
                {
                    _taskProvider = new TaskProvider();
                }
                return _taskProvider;
            }
        }

        public bool AddTask(Task task)
        {
            return TaskProvider.AddTask(EntityConverter.Convert(task));
        }

        public IEnumerable<Task> GetTasksBySender(int senderId)
        {
            return TaskProvider.GetTasksBySender(senderId).Select(EntityConverter.Convert);
        }

        public Task GetTaskById(int taskId)
        {
            return EntityConverter.Convert(TaskProvider.GetTasksById(taskId));
        }

        public IEnumerable<Task> GetTasks()
        {
            return TaskProvider.GetTasks().Select(EntityConverter.Convert);
        }

        public void UpdateTaskText(Task model)
        {
            TaskProvider.UpdateTaskText(EntityConverter.Convert(model));
        }

        public void DeleteTask(int taskId)
        {
            TaskProvider.DeleteTask(taskId);
        }

        public void ConfirmTask(int id)
        {
            TaskProvider.DeleteTask(id);
        }

        public int SenderCompleteTasksCount()
        {
            return TaskProvider.SenderCompleteTasksCount();
        }

        public List<Task> GetTasksForCurrrentUser()
        {
            return TaskProvider.GetTasksForCurrrentUser().Select(EntityConverter.Convert).ToList();
        }

        public int GetNotAssignedTasksCount()
        {
            return TaskProvider.GetNotAssignedTasksCount();
        }

        public IEnumerable<Priority> GetPriorityList()
        {
            return Enum.GetValues(typeof(Priority)).Cast<Priority>();
        }

        public bool UpdateTask(Task task)
        {
            return TaskProvider.UpdateTask(EntityConverter.Convert(task));
        }

        public IEnumerable<Comment> GetCommentsForTask(int taskId)
        {
            return TaskProvider.GetCommentsForTask(taskId).Select(EntityConverter.Convert);
        }

        public void AddComment(Comment comment)
        {
            TaskProvider.AddComment(EntityConverter.Convert(comment));
        }

        public IEnumerable<Task> GetOverdueTasks()
        {
            return GetTasks().Where(x => x.Deadline < DateTime.Now);
        }
    }
}
