using BookStore.Application.Contracts.Order.ShoppingCart;
using BookStore.Application.Contracts.Product;
using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BookStore.EFCore.Repository
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        private readonly DatabaseContext _context;

        public ShoppingCartRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }



        public List<ShoppingCartViewModel> GetShoppingCarts(string userId)
        {
            var carts = _context.ShoppingCarts
                .Where(x=>x.ApplicationUserId == userId)
                .Select(x => new ShoppingCartViewModel
                {
                    ProductId = x.ProductId,
                    Count = x.Count,
                    Id = x.Id,

                }).ToList()
                ;
            //foreach (var cart in carts)
            //{
            //    cart.Product = _context.Products
            //        .Select(x=>new ProductViewModel
            //        {
            //            Id = x.Id


            //        })
            //        .FirstOrDefault(x=>x.Id==cart.ProductId);
            //}

            return carts;
        }


    }
}
