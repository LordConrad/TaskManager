using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Models
{
    public class ChiefTaskList
    {
        public int TaskId { get; set; }

        public string TaskText { get; set; }

        public string SenderName { get; set; }

        public int? RecipientId { get; set; }

        public string RecipientName { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime? AcceptCompleteDate { get; set; }

        public string PriorityId { get; set; }

        public string ResultComment { get; set; }
    }

    public class ChiefTaskViewModel
    {
        public List<ChiefTaskList> ChiefTaskList { get; set; }

        public IEnumerable<SelectListItem> RecipientList { get; set; }

        public FilterTaskViewModel FilterTaskViewModel { get; set; }
    }
}