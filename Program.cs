using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CarsAPI.Models;
namespace CarsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
             using (var db = new CarsContext())
            {
                
                // Create
                Console.WriteLine("Inserting a new car");
                db.Add(new Car {Make="porsche",Model="911",Price=1000,Year=2011 });
                db.SaveChanges();

                // Read
               /*  Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();
 */
                // Update
                /* Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    });
                db.SaveChanges();
 */
                // Delete
               /*  Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges(); */
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
