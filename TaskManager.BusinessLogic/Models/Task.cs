using System;
using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskText { get; set; }
        public int SenderId { get; set; }
        public virtual UserProfile TaskSender { get; set; }
        public int? RecipientId { get; set; }
        public virtual UserProfile TaskRecipient { get; set; }
        public DateTime? AssignDateTime { get; set; }
        //public int TaskChiefId { get; set; }
        //public virtual UserProfile TaskChief { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? AcceptCpmpleteDate { get; set; }
        public bool IsRecipientViewed { get; set; }
        public int? PriorityId { get; set; }
        public virtual Priority TaskPriority { get; set; }
        public string ResultComment { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<TaskEventLog> TaskEeventLogs { get; set; }
    }
}
