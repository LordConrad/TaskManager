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
        private ITasksProvider taksProvider;

        public bool AddTask(TaskBl task)
        {
            return taksProvider.AddTask(EntityConverter.ConvertToTaskDal(task));
        }

        public IEnumerable<TaskBl> GetTasksBySender(int senderId)
        {
            return taksProvider.GetTasksBySender(senderId).Select(EntityConverter.ConvertToTaskBl);
        }

        public TaskBl GetTasksById(int taskId)
        {
            return EntityConverter.ConvertToTaskBl(taksProvider.GetTasksById(taskId));
        }

        public IEnumerable<TaskBl> GetTasks()
        {
            return taksProvider.GetTasks().Select(EntityConverter.ConvertToTaskBl);
        }

        public void UpdateTaskText(TaskBl model)
        {
            taksProvider.UpdateTaskText(EntityConverter.ConvertToTaskDal(model));
        }

        public void DeleteTask(int taskId)
        {
            taksProvider.DeleteTask(taskId);
        }

        public void ConfirmTask(int id)
        {
            taksProvider.DeleteTask(id);
        }

        public int SenderCompleteTasksCount()
        {
            return taksProvider.SenderCompleteTasksCount();
        }

        public List<TaskBl> GetTasksForCurrrentUser()
        {
            return taksProvider.GetTasksForCurrrentUser().Select(EntityConverter.ConvertToTaskBl).ToList();
        } 
    }
}
