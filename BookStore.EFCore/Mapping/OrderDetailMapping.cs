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
    public class OrderDetailMapping : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);

            builder.HasOne(o => o.OrderHeader)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(o => o.OrderHeaderId);

            builder.HasOne(o => o.Product)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(o => o.ProductId);

            

        }
    }
}
