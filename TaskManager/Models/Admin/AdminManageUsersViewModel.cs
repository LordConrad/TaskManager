using System.Collections.Generic;
using TaskManager.BusinessLogic.Models;

namespace TaskManager.Models.Admin
{
    public class AdminManageUsersViewModel : BaseViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}