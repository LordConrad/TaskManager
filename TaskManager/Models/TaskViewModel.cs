using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Models;

namespace TaskManager.Models
{
    public class TaskViewModel
    {

        public int TaskId { get; set; }
        public string TaskText { get; set; }

        public int SenderId { get; set; }
        public virtual UserViewModel TaskSender { get; set; }

        public int? RecipientId { get; set; }
        public virtual UserViewModel TaskRecipient { get; set; }

        public DateTime? AssignDateTime { get; set; }

        //public int TaskChiefId { get; set; }

        //public virtual UserProfile TaskChief { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? AcceptCpmpleteDate { get; set; }

        public bool IsRecipientViewed { get; set; }

        public int? PriorityId { get; set; }
        public Priority TaskPriority { get; set; }

        public string ResultComment { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
        public IEnumerable<TaskEventLogViewModel> TaskEeventLogs { get; set; }
    }
}
