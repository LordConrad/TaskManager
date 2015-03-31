using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface ITasksService
    {
        bool AddTask(TaskBl task);
        IEnumerable<TaskBl> GetTasksBySender(int senderId);
        TaskBl GetTasksById(int taskId);
        IEnumerable<TaskBl> GetTasks();
        void UpdateTaskText(TaskBl model);
        void DeleteTask(int taskId);
        void ConfirmTask(int id);
        int SenderCompleteTasksCount();
        List<TaskBl> GetTasksForCurrrentUser();
    }
}
