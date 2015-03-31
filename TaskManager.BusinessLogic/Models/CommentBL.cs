using System;

namespace TaskManager.BusinessLogic.Models
{
    public class CommentBl
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int TaskId { get; set; }
        virtual public TaskBl Task { get; set; }
        public int AuthorId { get; set; }
        virtual public UserModelBl Author { get; set; }
    }
}
