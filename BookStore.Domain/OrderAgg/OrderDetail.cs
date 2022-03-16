using BookStore.Domain.ProductAgg;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookStore.Domain.OrderAgg
{
    public class OrderDetail
    {
        public long Id { get; set; }

        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
