using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helpers;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Sender")]
    public class SenderController : Controller
    {
        //
        // GET: /Task/
        [HttpGet]
        public ActionResult Index()
        {

            return View(ModelHelper.GetTasksBySender(WebSecurity.CurrentUserId).Where(x => x.AcceptCpmpleteDate == null));
        }

        [HttpGet]
        public ActionResult NewTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewTask(NewTaskViewModel model)
        {
            var newTask = new Task
            {
                CreateDate = DateTime.Now,
                SenderId = WebSecurity.CurrentUserId,
                TaskText = model.TaskText.Trim()
            };
            if (!ModelHelper.AddTask(newTask))
            {
                ModelState.AddModelError("", "Произошла ошибка при добавлении новой заявки. Повторите попытку позже или обратитесь к администратору.");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int taskId)
        {
            var task = ModelHelper.GetTasksById(taskId);
            if (task == null)
            {
                //ModelState.AddModelError("","Указанная заявка не найдена");
                return RedirectToAction("Index");
            }
            var model = new NewTaskViewModel
            {
                TaskId = taskId,
                TaskText = task.TaskText,
                IsComlete = task.CompleteDate.HasValue,
                IsReadOnly = task.TaskRecipient != null,
                RecipientName = task.TaskRecipient != null ? task.TaskRecipient.UserFullName : "не назначен",
                AssignDate = task.AssignDateTime.HasValue ? task.AssignDateTime.Value.ToString(ModelHelper.DateTimeFormatFull) : string.Empty,
                CompleteDate  = task.CompleteDate.HasValue ? task.CompleteDate.Value.ToString(ModelHelper.DateTimeFormatFull) : string.Empty,
                Deadline = task.Deadline.HasValue ? task.Deadline.Value.ToString(ModelHelper.DateFormat) : string.Empty,
                ResultComment = (task.CompleteDate.HasValue && string.IsNullOrEmpty(task.ResultComment)) ? "выполнено" : task.ResultComment,
                CreationDate = task.CreateDate.ToString(ModelHelper.DateTimeFormatFull),
                CommentsCount = task.Comments.Count
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(NewTaskViewModel model)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var task = context.Tasks.FirstOrDefault(x => x.TaskId == model.TaskId);
                    if (task != null && !task.TaskText.Trim().Equals(model.TaskText.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        task.TaskEeventLogs.Add(new TaskEeventLog
                        {
                            EventDateTime = DateTime.Now,
                            PropertyName = "TaskText",
                            UserId = WebSecurity.CurrentUserId,
                            OldValue = task.TaskText,
                            NewValue = model.TaskText
                        });
                        task.TaskText = model.TaskText;
                        
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int taskId)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var task = context.Tasks.FirstOrDefault(x => x.TaskId == taskId);
                    
                    if (task != null)
                    {
                        context.Tasks.Remove(task);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }

        public ActionResult ConfirmTask(int id)
        {
            try
            {
                using (var context = new TaskManagerContext())
                {
                    var task = context.Tasks.FirstOrDefault(x => x.TaskId == id);
                    if (task != null)
                    {
                        task.TaskEeventLogs.Add(new TaskEeventLog
                        {
                            EventDateTime = DateTime.Now,
                            PropertyName = "AcceptCpmpleteDate",
                            UserId = WebSecurity.CurrentUserId,
                            OldValue = task.AcceptCpmpleteDate.ToString(),
                            NewValue = DateTime.Now.ToString()
                        });
                        task.AcceptCpmpleteDate = DateTime.Now;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult SenderCompleteTasksCount()
        {
            int count = 0;
            try
            {
                using (var context = new TaskManagerContext())
                {
                    count = context.Tasks.Count(x => x.SenderId == WebSecurity.CurrentUserId && x.CompleteDate.HasValue && !x.AcceptCpmpleteDate.HasValue);
                }
            }
            catch (Exception)
            {
            }
            var badge = new BadgeModel { Count = count };
            if (Session["SenderCompleteTasksCount"] != null && ((int)Session["SenderCompleteTasksCount"]) < count)
            {
                badge.IsPlay = true;
            }
            Session["SenderCompleteTasksCount"] = count;

            return PartialView(badge);
        }

    }
}
