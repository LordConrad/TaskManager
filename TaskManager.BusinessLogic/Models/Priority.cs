using System.Collections.Generic;
using System.ComponentModel;

namespace TaskManager.BusinessLogic.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        virtual public ICollection<Task> SamePriorityTasks { get; set; }
    }

    public enum PriorityEnum
    {
        [Description("высокий")]
        High,
        [Description("обычный")]
        Medium,
        [Description("низкий")]
        Low
    }

}