using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskManager.BusinessLogic.Services;
using TaskManager.Converters;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    public class StatisticController : Controller
    {
        private TasksService tasksService = new TasksService();

        //
        // GET: /Statistic/

        public ActionResult Index()
        {
            return View(tasksService.GetTasks().Select(EntityConverter.ConverttoTaskUi));
        }

    }
}
