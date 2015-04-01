using System;
using System.Linq;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.Converters;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
	[Authorize(Roles = "Sender")]
	public class SenderController : Controller
	{
		private readonly ITasksService _tasksService;

		public SenderController(ITasksService tasksService)
		{
			_tasksService = tasksService;
		}

		//
		// GET: /Task/
		[HttpGet]
		public ActionResult Index()
		{
			var query = _tasksService.GetTasksBySender(WebSecurity.CurrentUserId)
				.Where(x => x.AcceptCpmpleteDate == null);
			return System.Web.UI.WebControls.View(query.Select(EntityConverter.ConvertToTaskUi));
		}

		[HttpGet]
		public ActionResult NewTask()
		{
			return View();
		}

		[HttpPost]
		public ActionResult NewTask(NewTaskViewModel model)
		{
			var newTask = new TaskBl
			{
				CreateDate = DateTime.Now,
				SenderId = WebSecurity.CurrentUserId,
				TaskText = model.TaskText.Trim()
			};
			if (!_tasksService.AddTask(newTask))
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
			var task = _tasksService.GetTasksById(taskId);
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
				AssignDate = task.AssignDateTime.HasValue ? task.AssignDateTime.Value.ToString("dd.MM.yy  HH:mm") : string.Empty,
				CompleteDate = task.CompleteDate.HasValue ? task.CompleteDate.Value.ToString("dd.MM.yy  HH:mm") : string.Empty,
				Deadline = task.Deadline.HasValue ? task.Deadline.Value.ToString("dd.MM.yy") : string.Empty,
				ResultComment = (task.CompleteDate.HasValue && string.IsNullOrEmpty(task.ResultComment)) ? "выполнено" : task.ResultComment,
				CreationDate = task.CreateDate.ToString("dd.MM.yy  HH:mm"),
				CommentsCount = task.Comments.Count
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(NewTaskViewModel model)
		{
			_tasksService.UpdateTaskText(new TaskBl
			{
				TaskId = model.TaskId,
				TaskText = model.TaskText
			});
			return RedirectToAction("Index");
		}

		public ActionResult Delete(int taskId)
		{
			_tasksService.DeleteTask(taskId);
			return RedirectToAction("Index");
		}

		public ActionResult ConfirmTask(int id)
		{
			_tasksService.ConfirmTask(id);
			return RedirectToAction("Index");
		}

		public ActionResult SenderCompleteTasksCount()
		{
			var count = _tasksService.SenderCompleteTasksCount();

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
