using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface ITasksService
    {
        bool AddTask(TaskBL task);
        IEnumerable<TaskBL> GetTasksBySender(int senderId);
        TaskBL GetTasksById(int taskId);
        IEnumerable<TaskBL> GetTasks();
        void UpdateTaskText(TaskBL model);
        void DeleteTask(int taskId);
        void ConfirmTask(int id);
        int SenderCompleteTasksCount();
        List<TaskBL> GetTasksForCurrrentUser();
    }
}
