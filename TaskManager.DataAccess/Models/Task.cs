using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess.Models
{
	[Table("Task")]
	public class Task
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TaskId { get; set; }
		[Required]
		[MaxLength(1000)]
		public string TaskText { get; set; }
        
		[Required]
		public int SenderId { get; set; }
		[ForeignKey("SenderId")]
		public virtual UserProfile TaskSender { get; set; }

		public int? RecipientId { get; set; }
		[ForeignKey("RecipientId")]
		public virtual UserProfile TaskRecipient { get; set; }

		public DateTime? AssignDateTime { get; set; }
        
		//[Required]
		//public int TaskChiefId { get; set; }
		//[Required]
		//[ForeignKey("TaskChiefId")]
		//public virtual UserProfile TaskChief { get; set; }
        
		public DateTime? Deadline { get; set; }
		[Required]
		public DateTime CreateDate { get; set; }
		public DateTime? CompleteDate { get; set; }
		public DateTime? AcceptCpmpleteDate { get; set; }

		public bool IsRecipientViewed { get; set; }

		public int? PriorityId { get; set; }
		[ForeignKey("PriorityId")]
		public virtual Priority TaskPriority { get; set; }
        
		public string ResultComment { get; set; }

		[ForeignKey("AuthorId")]
		public virtual ICollection<Comment> Comments { get; set; }
		[ForeignKey("TaskLog")]
		public virtual ICollection<TaskEventLog> TaskEeventLogs { get; set; } 
	}
}