using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess;


namespace TaskManager.BusinessLogic.Models
{
    public class UserProfileBL
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; }

        //public int? ChiefId { get; set; }
        //public virtual UserProfile RecipChief { get; set; }

        //public virtual ICollection<UserProfile> ChiefRecipients { get; set; } 

        public virtual ICollection<TaskBL> SendedTasks { get; set; }

        public virtual ICollection<TaskBL> RecipTasks { get; set; }

        public virtual ICollection<TaskEventLogBl> Logs { get; set; }

        //[ForeignKey("TaskChiefId")]
        //public virtual ICollection<Task> ChiefTasks { get; set; }

        public virtual ICollection<CommentBL> Comments { get; set; }
    }
}
