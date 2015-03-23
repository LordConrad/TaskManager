﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.BusinessLogic.Models
{
    public class TaskEventLogBl
    {
            public int TaskEventLogId { get; set; }
            public DateTime EventDateTime { get; set; }
            public int UserId { get; set; }
            public virtual UserProfileBL User { get; set; }
            public int TaskId { get; set; }
            public virtual TaskBL Task { get; set; }
            public string PropertyName { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }

    }
}