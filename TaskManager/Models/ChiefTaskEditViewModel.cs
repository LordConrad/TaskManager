using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.Models
{
    public class ChiefTaskEditViewModel : BaseViewModel
    {
        public int TaskId { get; set; }

        public string TaskText { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Выберите ответственного за выполнение")]
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }

        public string AssignDate { get; set; }

        [Required(ErrorMessage = "Назначьте срок исполнения")]
        public string Deadline { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime? AcceptCompleteDate { get; set; }

        public Priority Priority { get; set; }

        public string ResultComment { get; set; }

        public string NewComment { get; set; }

        public IEnumerable<SelectListItem> RecipientsList { get; set; }
        public IEnumerable<SelectListItem> PrioritiesList { get; set; }

        public int CommentsCount { get; set; }
    }
}