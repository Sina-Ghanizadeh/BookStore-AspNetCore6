using BookStore.Application.Contracts.Order.ShoppingCart;

namespace BookStore.Domain.OrderAgg.Repository
{
    public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart>
    {

        List<ShoppingCartViewModel> GetShoppingCarts(string userId);
    }
}
