using System;

namespace TaskManager.BusinessLogic.Models
{
    public class TaskEventLog
    {
        public int TaskEventLogId { get; set; }
        public DateTime EventDateTime { get; set; }
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
