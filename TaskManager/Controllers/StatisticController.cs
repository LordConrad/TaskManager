using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Controllers
{
    public class StatisticController : Controller
    {
        //
        // GET: /Statistic/

        public ActionResult Index()
        {
            IEnumerable<Task> tasks;
            using (var context = new TaskManagerContext())
            {
                tasks = context.Tasks.Where(x => x.AcceptCpmpleteDate.HasValue)
                    .Include(x => x.TaskRecipient)
                    .ToList();
            }
            return View(tasks);
        }

    }
}
