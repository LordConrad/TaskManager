using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Models;

namespace TaskManager.Models
{
    public class TaskEventLogViewModel
    {
        public int TaskEventLogId { get; set; }
        public DateTime EventDateTime { get; set; }
        public int UserId { get; set; }
        public virtual UserViewModel User { get; set; }
        public int TaskId { get; set; }
        public virtual TaskViewModel Task { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
