using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Interfaces
{
    public interface ILogProvider
    {
        void AddLog(TaskEventLog log);
    }
}
