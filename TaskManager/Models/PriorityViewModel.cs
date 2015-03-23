using System.Collections.Generic;

namespace TaskManager.Models
{
    public class PriorityViewModel
    {

        public int PriorityId { get; set; }
        public string PriorityName { get; set; }

        virtual public ICollection<TaskViewModel> SamePriorityTasks { get; set; }
    }

    public enum PriorityEnum
    {
        High,
        Medium,
        Low
    }

}