using System.Collections.Generic;

namespace TaskManager.BusinessLogic.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public bool IsActive { get; set; }
    }
}
