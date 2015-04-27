using System;

namespace TaskManager.BusinessLogic.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskText { get; set; }
        public int SenderId { get; set; }
        public int? RecipientId { get; set; }
        public DateTime? AssignDateTime { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? AcceptCpmpleteDate { get; set; }
        public bool IsRecipientViewed { get; set; }
        public Priority TaskPriority { get; set; }
        public string ResultComment { get; set; }
    }
}
