using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int TaskId { get; set; }
        virtual public TaskViewModel Task { get; set; }
        public int AuthorId { get; set; }
        virtual public UserViewModel Author { get; set; }
    }
}
