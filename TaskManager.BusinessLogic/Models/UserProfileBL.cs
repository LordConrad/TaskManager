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

        public virtual ICollection<Task> SendedTasks { get; set; }

        public virtual ICollection<Task> RecipTasks { get; set; }

        public virtual ICollection<TaskEeventLog> Logs { get; set; }

        //[ForeignKey("TaskChiefId")]
        //public virtual ICollection<Task> ChiefTasks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
