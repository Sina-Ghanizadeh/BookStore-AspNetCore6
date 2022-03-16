using BookStore.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EFCore.Mapping
{
    public class AppliactionUserMapping : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x=>x.Name).IsRequired();

            builder.HasOne(x=>x.Company).WithMany(c=>c.ApplicationUsers).HasForeignKey(x=>x.CompanyId);
        }
    }
}
