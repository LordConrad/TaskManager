using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.Helpers;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Recipient")]
    public class RecipientController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogService _logService;
        private readonly IUserService _userService;

        public RecipientController(ITaskService taskService, ILogService logService, IUserService userService)
        {
            _taskService = taskService;
            _logService = logService;
            _userService = userService;
        }

        //
        // GET: /Recipient/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tasks()
        {
            var tasks = _taskService.GetTasksForCurrrentUser();

            var tasksViewModelList = tasks.Select(x => new RecipientTaskViewModel
            {
                AssignDateTime = x.AssignDateTime,
                CreationDate = x.CreateDate,
                Deadline = x.Deadline,
                Priority = x.Priority,
                SenderName = _userService.GetUserById(x.SenderId).UserFullName,
                TaskId = x.TaskId,
                TaskText = x.TaskText,
                IsViewed = x.IsRecipientViewed,
                IsComplete = x.CompleteDate.HasValue,
                ResultComment = x.ResultComment
            }).ToList();


            return PartialView(tasksViewModelList.OrderBy(x => x.IsViewed).ThenBy(x => x.Deadline));
        }

        [HttpGet]
        public ActionResult UndoComplete(int taskId)
        {
            try
            {
                var task = _taskService.GetTaskById(taskId);
                if (task != null)
                {
                    _logService.AddTaskEventLog(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "CompleteDate",
                        UserId = WebSecurity.CurrentUserId,
                        OldValue =
                            task.CompleteDate.HasValue ? task.CompleteDate.Value.ToString(Constants.DateFullFormat) : null,
                        NewValue = null
                    });
                    task.CompleteDate = null;
                    _taskService.UpdateTask(task);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RecipNewTasksCount()
        {
            int count = 0;
            count =
                _taskService.GetTasks()
                    .Count(x => x.RecipientId == WebSecurity.CurrentUserId && x.IsRecipientViewed == false);

            var badge = new BadgeModel { Count = count };
            if (Session["RecipNewTasksCount"] != null && ((int)Session["RecipNewTasksCount"]) < count)
            {
                badge.IsPlay = true;
            }
            Session["RecipNewTasksCount"] = count;

            return PartialView(badge);
        }

        public ActionResult Edit(int taskId)
        {
            try
            {
                var task = _taskService.GetTaskById(taskId);
                if (task != null)
                {
                    task.IsRecipientViewed = true;
                    _taskService.UpdateTask(task);
                }
                if (task != null)
                {
                    var taskEdit = new RecipTaskEditModel
                    {
                        AssignDateTime = task.AssignDateTime.Value,
                        CreationDate = task.CreateDate,
                        Deadline = task.Deadline.Value,
                        Priority = task.Priority,
                        SenderName = _userService.GetUserById(task.SenderId).UserFullName,
                        TaskId = task.TaskId,
                        TaskText = task.TaskText,
                        ResultComment = task.ResultComment,
                        CompleteDate = task.CompleteDate,
                        CommentsCount = _taskService.GetCommentsForTask(taskId).Count()
                    };
                    return View(taskEdit);
                }
            }
            catch (Exception)
            {

                //throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(RecipTaskEditModel model)
        {
            var task = _taskService.GetTaskById(model.TaskId);
            if (task != null)
            {
                if ((string.IsNullOrEmpty(task.ResultComment) && !string.IsNullOrEmpty(model.ResultComment))
                    ||
                    (!string.IsNullOrEmpty(task.ResultComment) &&
                     !task.ResultComment.Equals(model.ResultComment, StringComparison.InvariantCultureIgnoreCase)))
                {
                    _logService.AddTaskEventLog(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "ResultComment",
                        UserId = WebSecurity.CurrentUserId,
                        OldValue = task.ResultComment,
                        NewValue = model.ResultComment
                    });
                    task.ResultComment = model.ResultComment;
                }
                if (task.CompleteDate == null)
                {
                    _logService.AddTaskEventLog(new TaskEventLog
                    {
                        EventDateTime = DateTime.Now,
                        PropertyName = "CompleteDate",
                        UserId = WebSecurity.CurrentUserId,
                        OldValue =
                            task.CompleteDate.HasValue
                                ? task.CompleteDate.Value.ToString(Constants.DateFormat)
                                : null,
                        NewValue = DateTime.Now.ToString(Constants.DateFullFormat)
                    });
                    task.CompleteDate = DateTime.Now;
                }
                _taskService.UpdateTask(task);
            }

            return RedirectToAction("Index");
        }
    }
}
