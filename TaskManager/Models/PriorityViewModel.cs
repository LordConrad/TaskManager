using System.ComponentModel;

namespace TaskManager.Models
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