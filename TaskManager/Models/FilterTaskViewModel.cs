using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class FilterTaskViewModel
    {
        public string SearchText { get; set; }

        public string SelectedRecipient { get; set; }

        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }

        [Display(Name = "выполненные")]
        public bool CompleteFilter { get; set; }

        [Display(Name = "не обработанные")]
        public bool NotAssignedFilter { get; set; }

        [Display(Name = "просроченные")]
        public bool OverdueFilter { get; set; }

        [Display(Name = "закрытые")]
        public bool ArchiveFilter { get; set; }
    }
}