using BookStore.Application.Contracts.Order.ShoppingCart;
using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;
using BookStore.Domain.ProductAgg;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityProject.Application;

namespace BookStore.Application
{
    public class ShoppingCartApplication : IShoppingCartApplication
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public ShoppingCartApplication(IShoppingCartRepository cartRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public string AddToCart(CreateShoppingCart command)
        {
            var cartFromDb = _cartRepository
                .GetFirstOrDefault(c => c.ApplicationUserId == command.ApplicationUserId
                && c.ProductId == command.ProductId
                );
            if (cartFromDb == null)
            {
                ShoppingCart cart = new()
                {
                    ApplicationUserId = command.ApplicationUserId,
                    Count = command.Count,
                    ProductId = command.ProductId,
                };

                _cartRepository.Add(cart);
                _cartRepository.Save();
                return ApplicationMessages.AddToCart;

            }
            else
            {
                return IncrementCount(cartFromDb.Id, command.Count);
            }
        }

        public string DecrementCount(long id, int count)
        {
            var cartFromDb = _cartRepository.GetFirstOrDefault(c => c.Id == id);
            if (cartFromDb.Count <= 1)
            {

                _cartRepository.Delete(cartFromDb);
                _cartRepository.Save();

                return ApplicationMessages.RemoveCount;

            }
            else
            {
                cartFromDb.Count -= count;
                _cartRepository.Save();

                return ApplicationMessages.DecreamentCount;

            }

        }

        public void DeleteRange(string applicationUserId)
        {
            var carts = _cartRepository.GetAll(x => x.ApplicationUserId == applicationUserId);
            _cartRepository.DeleteRange(carts);
            _cartRepository.Save();

        }

        public List<ShoppingCartViewModel> GetShoppingCarts(string userId)
        {
            var carts = _cartRepository.GetShoppingCarts(userId);



            return carts;

        }

        public string IncrementCount(long id, int count)
        {
            var cartFromDb = _cartRepository.GetFirstOrDefault(c => c.Id == id);
            cartFromDb.Count += count;
            _cartRepository.Save();

            return ApplicationMessages.IncreamentCount;
        }

        public string Remove(long id)
        {
            var cartFromDb = _cartRepository.GetFirstOrDefault(c => c.Id == id);
            _cartRepository.Delete(cartFromDb);
            _cartRepository.Save();

            return ApplicationMessages.RemoveCount;
        }
    }
}
