using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public class UserProfileBl
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; }
        //public int? ChiefId { get; set; }
        //public virtual UserProfile RecipChief { get; set; }
        //public virtual ICollection<UserProfile> ChiefRecipients { get; set; } 
        public virtual ICollection<TaskBl> SendedTasks { get; set; }
        public virtual ICollection<TaskBl> RecipTasks { get; set; }
        public virtual ICollection<TaskEventLogBl> Logs { get; set; }
        //[ForeignKey("TaskChiefId")]
        //public virtual ICollection<Task> ChiefTasks { get; set; }
        public virtual ICollection<CommentBl> Comments { get; set; }
    }
}
