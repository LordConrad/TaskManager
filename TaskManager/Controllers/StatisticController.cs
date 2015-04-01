using System.Linq;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.Converters;

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
