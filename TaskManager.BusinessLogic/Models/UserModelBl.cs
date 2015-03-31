namespace TaskManager.BusinessLogic.Models
{
    public class UserModelBl
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string UserName { get; set; }
        public string ChiefId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSender { get; set; }
        public bool IsRecipient { get; set; }
        public bool IsChief { get; set; }
        public bool IsMasterChief { get; set; }
    }
}