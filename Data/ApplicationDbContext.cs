using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // This method will be used to seed the database
        public static class SeedData
        {
            public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
            {
                // Check if there are any categories already in the database
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "Work" },
                        new Category { Name = "Personal" },
                        new Category { Name = "Shopping" },
                        new Category { Name = "HouseWork"}
                    );
                    context.SaveChanges();
                }

                // Check if there are any priorities already in the database
                if (!context.Priorities.Any())
                {
                    context.Priorities.AddRange(
                        new Priority { Level = "Low" },
                        new Priority { Level = "Medium" },
                        new Priority { Level = "High" }
                    );
                    context.SaveChanges();
                }
            }
        }


    }




}
