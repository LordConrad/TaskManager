using System.Data.Entity;
using System.Web.Security;
using TaskManager.Models;
using WebMatrix.WebData;

namespace TaskManager.Migrations
{
    internal sealed class DatabaseInitializer : MigrateDatabaseToLatestVersion<TaskManagerContext, Configuration> 
    {
        
        public override void InitializeDatabase(TaskManagerContext context)
        {
            base.InitializeDatabase(context);
            SeedMembership();
        }

        private void SeedMembership()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", true);
            }

            if (WebSecurity.GetUserId("admin") == -1)
            {
                InitializeDefaultData();
            }
        }

        private void InitializeDefaultData()
        {
            WebSecurity.CreateUserAndAccount("admin", "qwe123", propertyValues: new
            {
                UserFullName = "Администратор",
                IsActive = true
            });
            
            Roles.CreateRole("Admin");
            Roles.CreateRole("Sender");
            Roles.CreateRole("Chief");
            Roles.CreateRole("Recipient");
            Roles.CreateRole("MasterChief");
            Roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            using (var context = new TaskManagerContext())
            {
                context.Priorities.Add(new Priority {PriorityName = "Низкий"});
                context.Priorities.Add(new Priority {PriorityName = "Средний"});
                context.Priorities.Add(new Priority {PriorityName = "Высокий"});
                context.SaveChanges();
            }
        }
    }
}