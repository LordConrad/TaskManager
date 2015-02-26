using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class RecipientTaskViewModel
    {
        public int TaskId { get; set; }

        public string TaskText { get; set; }

        public string SenderName { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime AssignDateTime { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsComplete { get; set; }

        public string ResultComment { get; set; }

        public int PriorityId { get; set; }

        public string PriorityName { get; set; }

        public bool IsViewed { get; set; }

    }
}