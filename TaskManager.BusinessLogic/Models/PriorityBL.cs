using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public class PriorityBl
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        virtual public ICollection<TaskBl> SamePriorityTasks { get; set; }
    }

    public enum PriorityEnum
    {
        High,
        Medium,
        Low
    }

}