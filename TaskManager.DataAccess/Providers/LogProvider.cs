using System.Data.Entity;
using TaskManager.DataAccess.Interfaces;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Providers
{
    public class LogProvider : ILogProvider
    {
        public void AddLog(TaskEventLog log)
        {
            using (var context = new TaskManagerContext())
            {
                context.Entry(log).State = EntityState.Added;
            }
        }
    }
}
