using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.ShoppingCart
{
    public class CreateShoppingCart
    {
        public int ProductId { get; set; }

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }

        public double  Price { get; set; }

    }
}
