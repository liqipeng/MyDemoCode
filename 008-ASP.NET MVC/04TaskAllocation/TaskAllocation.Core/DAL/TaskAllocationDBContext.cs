using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskAllocation.Core.DomainModel;

namespace TaskAllocation.Core.DAL
{
    public class TaskAllocationDBContext : DbContext
    {
        public TaskAllocationDBContext()
            : base("name=" + ConfigurationManager.AppSettings["TaskAllocationDBContext"])
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
