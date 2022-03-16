using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.ShoppingCart
{
    public interface IShoppingCartApplication 
    {
        string AddToCart(CreateShoppingCart command);

        string IncrementCount(long id , int count);
        string DecrementCount(long id , int count);
        string Remove(long id);
        List<ShoppingCartViewModel> GetShoppingCarts(string userId);
        void DeleteRange(string applicationUserId);
    }
}
