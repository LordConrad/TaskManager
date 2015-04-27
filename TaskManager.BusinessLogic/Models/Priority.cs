using System.Collections.Generic;
using System.ComponentModel;

namespace TaskManager.BusinessLogic.Models
{
    public enum Priority
    {
        [Description("высокий")]
        High,
        [Description("обычный")]
        Medium,
        [Description("низкий")]
        Low
    }

}