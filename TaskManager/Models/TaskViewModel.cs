using System;
using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.Models
{
    public class TaskViewModel : BaseViewModel
    {
        public int TaskId { get; set; }
        public string TaskText { get; set; }
        public int SenderId { get; set; }
        public UserProfile TaskSender { get; set; }
        public int? RecipientId { get; set; }
        public virtual UserProfile TaskRecipient { get; set; }
        public DateTime? AssignDateTime { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? AcceptCpmpleteDate { get; set; }
        public bool IsRecipientViewed { get; set; }
        public int? PriorityId { get; set; }
        public Priority TaskPriority { get; set; }
        public string ResultComment { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<TaskEventLog> TaskEeventLogs { get; set; }
    }
}
