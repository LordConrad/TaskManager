using TaskManager.BusinessLogic.Converters;
using TaskManager.BusinessLogic.Interfaces;
using TaskManager.BusinessLogic.Models;
using TaskManager.DataAccess.Interfaces;
using TaskManager.DataAccess.Providers;

namespace TaskManager.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private ILogProvider _logProvider;

        private ILogProvider LogProvider
        {
            get
            {
                if (_logProvider == null)
                {
                    _logProvider = new LogProvider();
                }
                return _logProvider;
            }
        }

        public void AddTaskEventLog(TaskEventLog log)
        {
            _logProvider.AddLog(EntityConverter.Convert(log));
        }
    }
}
