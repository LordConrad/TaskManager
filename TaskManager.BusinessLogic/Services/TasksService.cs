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

        public bool AddTask(TaskBL task)
        {
            return taksProvider.AddTask(EntityConverter.ConverttoTaskDal(task));
        }

        public IEnumerable<TaskBL> GetTasksBySender(int senderId)
        {
            return taksProvider.GetTasksBySender(senderId).Select(EntityConverter.ConverttoTaskBl);
        }

        public TaskBL GetTasksById(int taskId)
        {
            return EntityConverter.ConverttoTaskBl(taksProvider.GetTasksById(taskId));
        }

        public IEnumerable<TaskBL> GetTasks()
        {
            return taksProvider.GetTasks().Select(EntityConverter.ConverttoTaskBl);
        }

        public void UpdateTaskText(TaskBL model)
        {
            taksProvider.UpdateTaskText(EntityConverter.ConverttoTaskDal(model));
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

        public List<TaskBL> GetTasksForCurrrentUser()
        {
            return taksProvider.GetTasksForCurrrentUser().Select(EntityConverter.ConverttoTaskBl).ToList();
        } 
    }
}
