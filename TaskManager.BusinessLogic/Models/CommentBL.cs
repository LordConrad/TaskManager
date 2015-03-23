using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess;

namespace TaskManager.BusinessLogic.Models
{
    public class CommentBL
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public int TaskId { get; set; }
        virtual public TaskBL Task { get; set; }
        public int AuthorId { get; set; }
        virtual public UserModelBl Author { get; set; }
    }
}
