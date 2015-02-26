using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class RecipTaskEditModel
    {
        public int TaskId { get; set; }

        public string TaskText { get; set; }

        public string SenderName { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime AssignDateTime { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public string PriorityName { get; set; }

        [MaxLength(1000, ErrorMessage = "Максимальная длина комментария 1000 символов")]
        public string NewComment { get; set; }
        
        public string ResultComment { get; set; }

        public int CommentsCount { get; set; }

    }
}