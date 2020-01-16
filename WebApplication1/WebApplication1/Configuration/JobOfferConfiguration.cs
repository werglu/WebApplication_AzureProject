using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Configuration
{
    public class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {   //modelbuilder.ApplyConfiguration(new JobOfferConfiguration());
            builder.ToTable("Companies");
            builder.HasData
            (
                new JobOffer
                {
                    Id = 1,
                    Description = "description",
                    Salary = 3000,
                    JobTitle = "Driver",
                    Location = "Warsaw"
                },
                new JobOffer
                {
                    Id = 2,
                    Description = "description",
                    Salary = 3500,
                    JobTitle = "Teacher",
                    Location = "Lodz"
                },
                new JobOffer
                {
                    Id = 3,
                    Description = "description",
                    Salary = 6000,
                    JobTitle = "Doctor",
                    Location = "Gdansk"
                },
                new JobOffer
                {
                    Id = 4,
                    Description = "description",
                    Salary = 4000,
                    JobTitle = "Nurse",
                    Location = "Warsaw"
                }
            );
        }
    }
}
