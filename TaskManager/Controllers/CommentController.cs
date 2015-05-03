using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.Models;
using TaskManager.Converters;

namespace TaskManager.Controllers
{
    public class CommentController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogService _logService;

        public CommentController(ITaskService taskService, ILogService logService)
        {
            _taskService = taskService;
            _logService = logService;
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
                    var task = _taskService.GetTaskById(model.TaskId);
                    if (task != null)
                    {
                        _taskService.AddComment(new Comment
                        {
                            AuthorId = model.AuthorId,
                            CommentText = model.Text.Trim(),
                            CommentDate = DateTime.Now
                        });
                        _taskService.UpdateTask(task);
                        return new JsonResult() { Data = new { result = "success" } };
                    }
                }

                catch (Exception)
                {
                    throw;
                }
            }
            else
            {

            }
            return new JsonResult() { Data = new { result = "error" } };
        }

    }
}
