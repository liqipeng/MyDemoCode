using EFDemo.MultipleDB.Models;
using EFDemo.MultipleDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.MultipleDB.Services
{
    public class BloggingService
    {
        public bool Add(Blog blog) 
        {
            using (BloggingContext ctx = new BloggingContext()) 
            {
                ctx.Blogs.Add(blog);

                ctx.SaveChanges();
            }

            return true;
        }
    }
}
