using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess.Models
{
	[Table("Priority")]
	public class Priority
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PriorityId { get; set; }
		public string PriorityName { get; set; }
		[ForeignKey("PriorityId")]
		virtual public ICollection<Task> SamePriorityTasks { get; set; }
	}
}