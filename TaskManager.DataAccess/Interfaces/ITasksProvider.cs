using System.Collections.Generic;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Interfaces
{
    public interface ITasksProvider
    {
        bool AddTask(Task task);
        IEnumerable<Task> GetTasksBySender(int senderId);
        Task GetTasksById(int taskId);
        IEnumerable<Task> GetTasks();
        IList<Task> GetTasksForCurrrentUser();
        void UpdateTaskText(Task model);
        void DeleteTask(int taskId);
        void ConfirmTask(int id);
        int SenderCompleteTasksCount();

    }
}
