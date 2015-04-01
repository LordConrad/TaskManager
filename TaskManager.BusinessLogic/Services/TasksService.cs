using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.BusinessLogic.Converters;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Interfaces;

namespace TaskManager.BusinessLogic.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksProvider _taksProvider;

	    public TasksService(ITasksProvider taksProvider)
	    {
		    this._taksProvider = taksProvider;
	    }

	    public bool AddTask(TaskBl task)
        {
            return _taksProvider.AddTask(EntityConverter.ConvertToTaskDal(task));
        }

        public IEnumerable<TaskBl> GetTasksBySender(int senderId)
        {
            return _taksProvider.GetTasksBySender(senderId).Select(EntityConverter.ConvertToTaskBl);
        }

        public TaskBl GetTasksById(int taskId)
        {
            return EntityConverter.ConvertToTaskBl(_taksProvider.GetTasksById(taskId));
        }

        public IEnumerable<TaskBl> GetTasks()
        {
            return _taksProvider.GetTasks().Select(EntityConverter.ConvertToTaskBl);
        }

        public void UpdateTaskText(TaskBl model)
        {
            _taksProvider.UpdateTaskText(EntityConverter.ConvertToTaskDal(model));
        }

        public void DeleteTask(int taskId)
        {
            _taksProvider.DeleteTask(taskId);
        }

        public void ConfirmTask(int id)
        {
            _taksProvider.DeleteTask(id);
        }

        public int SenderCompleteTasksCount()
        {
            return _taksProvider.SenderCompleteTasksCount();
        }

        public List<TaskBl> GetTasksForCurrrentUser()
        {
            return _taksProvider.GetTasksForCurrrentUser().Select(EntityConverter.ConvertToTaskBl).ToList();
        } 
    }
}
