using System.Collections.Generic;

namespace TaskManager.Models
{
    public class BaseViewModel
    {
        public string CurrentUserName { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}