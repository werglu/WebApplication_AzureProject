using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {   //modelbuilder.ApplyConfiguration(new CompanyConfiguration());
            builder.ToTable("Companies");
            builder.HasData
            (
                new Company
                {
                    Id = 1,
                    Name = "Big company"
                },
                new Company
                {
                    Id = 2,
                    Name = "Jane Company"
                },
                new Company
                {
                    Id = 3,
                    Name = "Mike Company"
                },
                new Company
                {
                    Id = 4,
                    Name = "Monkky"
                }
            );
        }
    }
}
