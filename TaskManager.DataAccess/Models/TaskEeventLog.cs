using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataAccess.Models
{
	[Table("TaskEventLog")]
    public class TaskEeventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskEventLogId { get; set; }
        [Required]
        public DateTime EventDateTime { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("LogUser")]
        public virtual UserProfile User { get; set; }
        [Required]
        public int TaskId { get; set; }
        [ForeignKey("TaskLog")]
        public virtual Task Task { get; set; }
        [Required]
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }



}