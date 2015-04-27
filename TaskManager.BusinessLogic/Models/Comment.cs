using System;

namespace TaskManager.BusinessLogic.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int TaskId { get; set; }
        public int AuthorId { get; set; }
    }
}
