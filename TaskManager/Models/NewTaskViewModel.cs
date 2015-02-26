using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class NewTaskViewModel
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "Введите текст заявки")]
        [MaxLength(1000, ErrorMessage = "Максимальная длина текста заявки 1000 символов")]
        [Display(Name = "Текст заявки")]
        public string TaskText { get; set; }

        public string CreationDate { get; set; }
        public string RecipientName { get; set; }
        public string AssignDate { get; set; }
        public string CompleteDate { get; set; }
        public string Deadline { get; set; }
        public string ResultComment { get; set; }

        public string NewComment { get; set; }

        public bool IsReadOnly { get; set; }
        public bool IsComlete { get; set; }
        public bool IsAcceptComlete { get; set; }

        public int CommentsCount { get; set; }
    }
}