using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocation.Core.DomainModel;

namespace TaskAllocation.Core.DAL
{
    public sealed class Configuration : DbMigrationsConfiguration<TaskAllocationDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TaskAllocation.Core.TaskDeliveryDBContext";
        }

        protected override void Seed(TaskAllocationDBContext context)
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

            User adminUser = context.Users.Where(u => u.Name == "admin").FirstOrDefault();
            if (adminUser == null) 
            {
                context.Users.Add(new User() { Name = "admin", Password = "admin" });
            }
        }
    }
}
