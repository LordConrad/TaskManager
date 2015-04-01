using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "MasterChief")]
    public class MasterChiefController : Controller
    {
        public ActionResult Index(MasterChiefViewModel model)
        {
            if (model == null)
            {
                model = new MasterChiefViewModel();
            }
            if (model.MasterChiefTaskList == null)
            {
                model.MasterChiefTaskList = new List<MasterChiefTaskList>();
            }

            if (model.FilterTaskViewModel == null)
            {
                model.FilterTaskViewModel = new FilterTaskViewModel
                {
                    ArchiveFilter = false,
                    CompleteFilter = false,
                    StartDateFilter = null,
                    EndDateFilter = null,
                    NotAssignedFilter = false,
                    OverdueFilter = false,
                    SearchText = string.Empty,
                    SelectedRecipient = string.Empty
                };
            }
            List<Task> tasks;
            try
            {
                using (var context = new TaskManagerContext())
                {
                    tasks = context.Tasks.Where(x => model.FilterTaskViewModel.ArchiveFilter ? x.AcceptCpmpleteDate != null : x.AcceptCpmpleteDate == null)
                        .Include(x => x.Comments)
                        .Include(x => x.TaskSender)
                        .Include(x => x.TaskRecipient)
                        .Include(x => x.TaskPriority)
                        .ToList();
                    if (model.FilterTaskViewModel.CompleteFilter)
                    {
                        tasks = tasks.Where(x => x.CompleteDate != null).ToList();
                    }
                    if (model.FilterTaskViewModel.NotAssignedFilter)
                    {
                        tasks = tasks.Where(x => x.RecipientId == null).ToList();
                    }
                    if (model.FilterTaskViewModel.OverdueFilter)
                    {
                        tasks = tasks.Where(x => x.Deadline.HasValue && x.Deadline.Value.Date < DateTime.Now.Date).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.FilterTaskViewModel.SelectedRecipient) && model.FilterTaskViewModel.SelectedRecipient != "0")
                    {
                        tasks = tasks.Where(x => x.RecipientId.HasValue && x.RecipientId.ToString().Equals(model.FilterTaskViewModel.SelectedRecipient)).ToList();
                    }
                    if (model.FilterTaskViewModel.StartDateFilter.HasValue)
                    {
                        tasks = tasks.Where(x => x.CreateDate >= model.FilterTaskViewModel.StartDateFilter.Value).ToList();
                    }
                    if (model.FilterTaskViewModel.EndDateFilter.HasValue)
                    {
                        tasks = tasks.Where(x => x.AcceptCpmpleteDate.HasValue && x.AcceptCpmpleteDate <= model.FilterTaskViewModel.EndDateFilter.Value).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.FilterTaskViewModel.SearchText))
                    {
                        tasks = tasks.Where(x => x.TaskText.ToLower().Contains(model.FilterTaskViewModel.SearchText.ToLower())).ToList();
                    }
                    model.RecipientList = ModelHelper.GetRecipientsSelectedList("Все исполнители", "0", context);
                }
            }
            catch (Exception)
            {

                throw;
            }

            tasks.ForEach(x => model.MasterChiefTaskList.Add(new MasterChiefTaskList
            {
                AcceptCompleteDate = x.AcceptCpmpleteDate,
                CompleteDate = x.CompleteDate,
                CreationDate = x.CreateDate,
                Deadline = x.Deadline,
                PriorityId = x.TaskPriority != null ? x.TaskPriority.PriorityId.ToString() : "0",
                RecipientId = x.TaskRecipient != null ? x.TaskRecipient.UserId : (int?)null,
                RecipientName = x.TaskRecipient != null ? x.TaskRecipient.UserFullName : "не назначен",
                ResultComment = x.ResultComment,
                SenderName = x.TaskSender.UserFullName,
                TaskText = x.TaskText,
                TaskId = x.TaskId
            }));

            model.MasterChiefTaskList = model.MasterChiefTaskList.OrderBy(x => x.RecipientId.HasValue).ThenByDescending(x => x.CreationDate).ThenBy(x => x.PriorityId).ThenBy(x => x.Deadline).ToList();

            if (Request.IsAjaxRequest())
            {
                return PartialView("TaskListAjax", model);
            }
            return View(model);
        }

        public ActionResult Edit(int taskId)
        {
            try
            {
                Task task;
                using (var context = new TaskManagerContext())
                {
                    task = context.Tasks
                        .Include(x => x.TaskRecipient)
                        .Include(x => x.TaskSender)
                        .Include(x => x.TaskPriority)
                        .FirstOrDefault(x => x.TaskId == taskId);
                    if (task != null)
                    {
                        var taskViewModel = new ChiefTaskEditViewModel
                        {
                            AcceptCompleteDate = task.AcceptCpmpleteDate,
                            CompleteDate = task.CompleteDate,
                            CreationDate = task.CreateDate,
                            Deadline = task.Deadline.HasValue ? task.Deadline.Value.ToString(ModelHelper.DateFormat) : string.Empty,
                            PriorityId = task.TaskPriority != null ? task.TaskPriority.PriorityId.ToString() : "2",
                            PriorityName = task.TaskPriority != null ? task.TaskPriority.PriorityName : string.Empty,
                            RecipientId = task.TaskRecipient != null ? task.TaskRecipient.UserId.ToString() : "0",
                            RecipientName = task.TaskRecipient != null ? task.TaskRecipient.UserFullName : string.Empty,
                            AssignDate = task.AssignDateTime.HasValue ? task.AssignDateTime.Value.ToString(ModelHelper.DateTimeFormatFull) : string.Empty,
                            ResultComment = task.ResultComment,
                            SenderName = task.TaskSender.UserFullName,
                            TaskText = task.TaskText,
                            TaskId = task.TaskId,
                            CommentsCount = task.Comments.Count
                        };



                        taskViewModel.RecipientsList = ModelHelper.GetRecipientsSelectedList("не назначен", taskViewModel.PriorityId, context);

                        taskViewModel.PrioritiesList = ModelHelper.GetPrioritiesSelectedList(taskViewModel.PriorityId, context);


                        return View(taskViewModel);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult OverdueTasksCount()
        {
            int count;
            try
            {
                using (var context = new TaskManagerContext())
                {
                    count = context.Tasks.Where(x => x.Deadline.HasValue).ToList().Count(y => y.Deadline.Value.Date < DateTime.Now.Date);
                }
            }
            catch (Exception)
            {

                throw;
            }
            var res = new BadgeModel { Count = count };
            if (Session["NewTasksForManage"] != null && ((int)Session["NewTasksForManage"]) < count)
            {
                res.IsPlay = true;
            }
            Session["OverdueTasksForManage"] = count;
            return PartialView(res);
        }
    }
}