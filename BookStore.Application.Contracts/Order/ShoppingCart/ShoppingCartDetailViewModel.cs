using BookStore.Application.Contracts.Order.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.ShoppingCart
{
    public class ShoppingCartDetailViewModel
    {
        public IEnumerable<ShoppingCartViewModel> CartItems { get; set; }
        public OrderHeaderViewModel OrderHeader { get; set; }
    }
}
