using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface ITaskService
    {
        bool AddTask(Task task);
        IEnumerable<Task> GetTasksBySender(int senderId);
        Task GetTaskById(int taskId);
        IEnumerable<Task> GetTasks();
        void UpdateTaskText(Task model);
        void DeleteTask(int taskId);
        void ConfirmTask(int id);
        int SenderCompleteTasksCount();
        List<Task> GetTasksForCurrrentUser();
        int GetNotAssignedTasksCount();
        IEnumerable<Priority> GetPriorityList();
        bool UpdateTask(Task task);
        IEnumerable<Comment> GetCommentsForTask(int taskId);
        void AddComment(Comment comment);
        IEnumerable<Task> GetOverdueTasks();
    }
}
