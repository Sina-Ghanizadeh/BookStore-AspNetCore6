using BookStore.Domain.CoverTypeAgg;
using BookStore.Domain.OrderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EFCore.Mapping
{
    public class CoverTypeMapping : IEntityTypeConfiguration<CoverType>
    {
        public void Configure(EntityTypeBuilder<CoverType> builder)
        {
            builder.ToTable("CoverTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);

            

        }
    }
}
