using EFDemo.MultipleDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.MultipleDB.Repositories
{
    public class BloggingContext : DbContext
    {
        public BloggingContext()
            : base("name=" + ConfigurationManager.AppSettings["BloggingContext"])
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    } 
}
