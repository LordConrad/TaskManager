using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.BusinessLogic.Converters;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Interfaces;

namespace TaskManager.BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITasksProvider _taskProvider;

	    public TaskService(ITasksProvider taskProvider)
	    {
		    _taskProvider = taskProvider;
	    }

	    public bool AddTask(Task task)
        {
            return _taskProvider.AddTask(EntityConverter.Convert(task));
        }

        public IEnumerable<Task> GetTasksBySender(int senderId)
        {
            return _taskProvider.GetTasksBySender(senderId).Select(EntityConverter.Convert);
        }

        public Task GetTaskById(int taskId)
        {
            return EntityConverter.Convert(_taskProvider.GetTasksById(taskId));
        }

        public IEnumerable<Task> GetTasks()
        {
            return _taskProvider.GetTasks().Select(EntityConverter.Convert);
        }

        public void UpdateTaskText(Task model)
        {
            _taskProvider.UpdateTaskText(EntityConverter.Convert(model));
        }

        public void DeleteTask(int taskId)
        {
            _taskProvider.DeleteTask(taskId);
        }

        public void ConfirmTask(int id)
        {
            _taskProvider.DeleteTask(id);
        }

        public int SenderCompleteTasksCount()
        {
            return _taskProvider.SenderCompleteTasksCount();
        }

        public List<Task> GetTasksForCurrrentUser()
        {
            return _taskProvider.GetTasksForCurrrentUser().Select(EntityConverter.Convert).ToList();
        }

        public int GetNotAssignedTasksCount()
        {
            return _taskProvider.GetNotAssignedTasksCount();
        }

        public IEnumerable<Priority> GetPriorityList()
        {
            return Enum.GetValues(typeof(Priority)).Cast<Priority>();
        }

        public bool UpdateTask(Task task)
        {
            return _taskProvider.UpdateTask(EntityConverter.Convert(task));
        }

        public IEnumerable<Comment> GetCommentsForTask(int taskId)
        {
            return _taskProvider.GetCommentsForTask(taskId).Select(EntityConverter.Convert);
        }

        public IEnumerable<Task> GetOverdueTasks()
        {
            return GetTasks().Where(x => x.Deadline < DateTime.Now);
        }
    }
}
