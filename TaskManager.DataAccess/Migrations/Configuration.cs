using System.Data.Entity.Migrations;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.TaskManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TaskManager.Models.TaskManagerContext";
        }

        protected override void Seed(TaskManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
