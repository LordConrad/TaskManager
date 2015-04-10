using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess.Models
{
	[Table("UserProfile")]
	public class UserProfile
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserId { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string UserFullName { get; set; }
		[Required]
		public bool IsActive { get; set; }

		//public int? ChiefId { get; set; }
		//[ForeignKey("ChiefId")]
		//public virtual UserProfile RecipChief { get; set; }
        
		//[ForeignKey("ChiefId")]
		//public virtual ICollection<UserProfile> ChiefRecipients { get; set; } 
        
		[ForeignKey("SenderId")]
		public virtual ICollection<Task> SendedTasks { get; set; }
        
		[ForeignKey("RecipientId")]
		public virtual ICollection<Task> RecipTasks { get; set; }

		[ForeignKey("LogUser")]
		public virtual ICollection<TaskEventLog> Logs { get; set; }
        
		//[ForeignKey("TaskChiefId")]
		//public virtual ICollection<Task> ChiefTasks { get; set; }

		[ForeignKey("AuthorId")]
		public virtual ICollection<Comment> Comments { get; set; }
	}
}