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
    public class OrderHeaderMapping : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeaders");
            builder.HasKey(o=>o.Id);

            builder.Property(o=>o.OrderDate).IsRequired();
            builder.Property(o=>o.Name).IsRequired();
            builder.Property(o=>o.PhoneNumber).IsRequired();
            builder.Property(o=>o.StreetAddress).IsRequired();
            builder.Property(o=>o.City).IsRequired();
            builder.Property(o=>o.State).IsRequired();
            builder.Property(o=>o.PostalCode).IsRequired();

            builder.HasMany(x=>x.OrderDetails)
                .WithOne(x=>x.OrderHeader)
                .HasForeignKey(x=>x.OrderHeaderId);
        }
    }
}
