using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager.BusinessLogic.Models
{
    public class PriorityBL
    {

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        virtual public ICollection<TaskBL> SamePriorityTasks { get; set; }
    }

    public enum PriorityEnum
    {
        High,
        Medium,
        Low
    }

}