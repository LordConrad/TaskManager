using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace TaskManager.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<TaskManagerContext>
    {
        
        public override void InitializeDatabase(TaskManagerContext context)
        {
            base.InitializeDatabase(context);
            SeedMembership();
        }

        protected override void Seed(TaskManagerContext context)
        {
            base.Seed(context);
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