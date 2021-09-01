using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Models
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st book",
                        DateAdded = System.DateTime.Now,
                        Description = "Red",
                        IsRead = true,
                        Genre = "Comedy",
                        DateRead = System.DateTime.Now.AddDays(10),
                        CoverUrl = "https://",
                        Rate = 100
                    } ,
                    new Book()
                    {
                        Title = "1st book",
                        DateAdded = System.DateTime.Now,
                        Description = "Red",
                        IsRead = false,
                        Genre = "Comedy",
                        CoverUrl = "https://",
                        Rate = 100
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
