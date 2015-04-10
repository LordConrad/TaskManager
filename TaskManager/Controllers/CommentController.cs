using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class CommentController : Controller
    {
        private readonly ITaskService _taskService;

        public CommentController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public ActionResult GetCommentsForTask(int taskId)
        {
            var comments = _taskService.GetCommentsForTask(taskId);
            

            return PartialView(comments);
        }

        [HttpPost]
        public ActionResult AddNewComment(CommentAddModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.Text) && model.Text.Trim().Length > 1000)
            {
                return new JsonResult() { Data = new { result = "maxLenghtError", validator = "NewComment", message = "Максимальная длина комментария 1000 символов" } };
            }

            if (model != null && model.AuthorId != 0 && model.TaskId != 0 && !string.IsNullOrEmpty(model.Text))
            {
                try
                {
                    using (var context = new TaskManagerContext())
                    {
                        var task = context.Tasks.Include(x => x.Comments).FirstOrDefault(x => x.TaskId == model.TaskId);
                        if (task != null)
                        {
                            task.Comments.Add(new Comment
                            {
                                AuthorId = model.AuthorId,
                                CommentText = model.Text.Trim(),
                                CommentDate = DateTime.Now
                            });
                            context.SaveChanges();
                            return new JsonResult(){Data = new { result = "success" }};
                        }
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            else
            {
                
            }
            return new JsonResult(){Data = new { result = "error"}};
        }

    }
}
