using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebApplication1Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebApplication1Context>>()))
            {
                // Look for any job offers.
                if (context.JobOffer.Any())
                {
                    return;   // DB has been seeded
                }

                context.JobOffer.AddRange(
                    new JobOffer
                    {
                        JobTitle = "Backend Developer",
                        Salary = 6000
                    },

                    new JobOffer
                    {
                        JobTitle = "Frontend Developer",
                        Salary = 5000
                    },

                    new JobOffer
                    {
                        JobTitle = "Teacher",
                        Salary = 3000
                    },

                    new JobOffer
                    {
                        JobTitle = "Cook",
                        Salary = 3500
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
