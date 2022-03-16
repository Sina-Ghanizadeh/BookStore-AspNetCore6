using BookStore.Application.Contracts.Product;

namespace BookStore.Application.Contracts.Order.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public long Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }

        public double Price { get; set; }
    }
}
