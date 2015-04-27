using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Task> SendedTasks { get; set; }
        public IEnumerable<Task> RecipTasks { get; set; }
        public IEnumerable<TaskEventLog> Logs { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsChief { get; set; }
        public bool IsMasterChief { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsSender { get; set; }
    }
}
