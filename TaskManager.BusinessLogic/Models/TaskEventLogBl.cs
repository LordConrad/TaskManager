using System;

namespace TaskManager.BusinessLogic.Models
{
    public class TaskEventLogBl
    {
        public int TaskEventLogId { get; set; }
        public DateTime EventDateTime { get; set; }
        public int UserId { get; set; }
        public virtual UserProfileBl User { get; set; }
        public int TaskId { get; set; }
        public virtual TaskBl Task { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
