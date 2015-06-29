using EFDemo.MultipleDB.Models;
using EFDemo.MultipleDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.MultipleDB.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Initializer.Initialize();

            BloggingService bloggingService = new BloggingService();
            bloggingService.Add(new Blog()
            {
                Name = "How to use EF",
                Posts = new List<Post>() 
                {
                    new Post()
                    {
                        Title = "Good Article",
                        Content = "It's what i am looking for."
                    }
                }
            });
            Console.WriteLine("OK");

            Console.ReadKey();
        }
    }
}
