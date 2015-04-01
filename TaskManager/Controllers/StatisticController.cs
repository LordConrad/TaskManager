using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Services;
using TaskManager.Converters;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
	public class StatisticController : Controller
	{
		private readonly ITasksService _tasksService;

		public StatisticController(ITasksService tasksService)
		{
			_tasksService = tasksService;
		}

		//
		// GET: /Statistic/

		public ActionResult Index()
		{
			return View(_tasksService.GetTasks().Select(EntityConverter.ConvertToTaskUi));
		}

	}
}
