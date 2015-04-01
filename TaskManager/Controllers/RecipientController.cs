using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using TaskManager.Helpers;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
	[Authorize(Roles = "Recipient")]
	public class RecipientController : Controller
	{
		private readonly ITasksService _tasksService;

		public RecipientController(ITasksService tasksService)
		{
			_tasksService = tasksService;
		}

		//
		// GET: /Recipient/

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Tasks()
		{
			var tasks = _tasksService.GetTasksForCurrrentUser();

			var tasksViewModelList = new List<RecipientTaskViewModel>();

			foreach (var x in tasks)
				tasksViewModelList.Add(new RecipientTaskViewModel
					{
						AssignDateTime = x.AssignDateTime,
						CreationDate = x.CreateDate,
						Deadline = x.Deadline,
						PriorityId = x.PriorityId,
						PriorityName = x.TaskPriority.PriorityName,
						SenderName = x.TaskSender.UserFullName,
						TaskId = x.TaskId,
						TaskText = x.TaskText,
						IsViewed = x.IsRecipientViewed,
						IsComplete = x.CompleteDate.HasValue,
						ResultComment = x.ResultComment
					});



			return PartialView(tasksViewModelList.OrderBy(x => x.IsViewed).ThenBy(x => x.Deadline));
		}

		[HttpGet]
		public ActionResult UndoComplete(int taskId)
		{
			try
			{
				using (var context = new TaskManagerContext())
				{
					var task = context.Tasks.Include(x => x.TaskEeventLogs).FirstOrDefault(x => x.TaskId == taskId);
					if (task != null)
					{
						task.TaskEeventLogs.Add(new TaskEeventLog
						{
							EventDateTime = DateTime.Now,
							PropertyName = "CompleteDate",
							UserId = WebSecurity.CurrentUserId,
							OldValue = task.CompleteDate.HasValue ? task.CompleteDate.Value.ToString(ModelHelper.DateTimeFormatFull) : null,
							NewValue = null
						});
						task.CompleteDate = null;
						context.SaveChanges();
					}
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
			try
			{
				using (var context = new TaskManagerContext())
				{
					count = context.Tasks.Count(x => x.RecipientId == WebSecurity.CurrentUserId && x.IsRecipientViewed == false);
				}
			}
			catch (Exception)
			{
			}
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
				Task task;
				using (var context = new TaskManagerContext())
				{
					task = context.Tasks.Include(x => x.TaskSender)
						.Include(x => x.TaskPriority)
						.Include(x => x.TaskSender)
						.Include(x => x.Comments)
						.FirstOrDefault(x => x.TaskId == taskId);
					if (task != null)
					{
						task.IsRecipientViewed = true;
						context.SaveChanges();
					}


				}
				if (task != null)
				{
					var taskEdit = new RecipTaskEditModel
					{
						AssignDateTime = task.AssignDateTime.Value,
						CreationDate = task.CreateDate,
						Deadline = task.Deadline.Value,
						PriorityName = task.TaskPriority.PriorityName,
						SenderName = task.TaskSender.UserFullName,
						TaskId = task.TaskId,
						TaskText = task.TaskText,
						ResultComment = task.ResultComment,
						CompleteDate = task.CompleteDate,
						CommentsCount = task.Comments.Count
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
			try
			{
				using (var context = new TaskManagerContext())
				{
					var task = context.Tasks.Include(x => x.TaskEeventLogs).FirstOrDefault(x => x.TaskId == model.TaskId);
					if (task != null)
					{
						if ((string.IsNullOrEmpty(task.ResultComment) && !string.IsNullOrEmpty(model.ResultComment))
							|| (!string.IsNullOrEmpty(task.ResultComment) && !task.ResultComment.Equals(model.ResultComment, StringComparison.InvariantCultureIgnoreCase)))
						{
							task.TaskEeventLogs.Add(new TaskEeventLog
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
							task.TaskEeventLogs.Add(new TaskEeventLog
							{
								EventDateTime = DateTime.Now,
								PropertyName = "CompleteDate",
								UserId = WebSecurity.CurrentUserId,
								OldValue = task.CompleteDate.HasValue ? task.CompleteDate.Value.ToString(ModelHelper.DateTimeFormatFull) : null,
								NewValue = DateTime.Now.ToString(ModelHelper.DateTimeFormatFull)
							});
							task.CompleteDate = DateTime.Now;
						}
						context.SaveChanges();
					}
				}
			}
			catch (Exception)
			{

				throw;
			}

			return RedirectToAction("Index");
		}
	}
}
