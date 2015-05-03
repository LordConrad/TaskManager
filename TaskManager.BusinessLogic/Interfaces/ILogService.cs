using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.BusinessLogic.Interfaces
{
    public interface ILogService
    {
        void AddTaskEventLog(TaskEventLog log);
    }
}
