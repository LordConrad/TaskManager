using System.Collections.Generic;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Interfaces
{
    public interface ITaskProvider
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
        int GetNotAssignedTasksCount();
        IEnumerable<Priority> GetPriorityList();
        bool UpdateTask(Task task);
        IEnumerable<Comment> GetCommentsForTask(int taskId);
        void AddComment(Comment comment);
    }
}
