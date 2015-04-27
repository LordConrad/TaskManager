using System.Linq;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.Converters;

namespace TaskManager.Controllers
{
	public class StatisticController : Controller
	{
		private readonly ITaskService _taskService;

		public StatisticController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		//
		// GET: /Statistic/

		public ActionResult Index()
		{
			return View(_taskService.GetTasks().Select(EntityConverter.Convert));
		}

	}
}
