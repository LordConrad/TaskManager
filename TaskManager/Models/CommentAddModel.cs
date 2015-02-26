using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class CommentAddModel
    {
        public int TaskId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
    }
}