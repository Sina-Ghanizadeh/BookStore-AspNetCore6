using BookStore.Application.Contracts.Order.ShoppingCart;
using BookStore.Application.Contracts.Product;
using BookStore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using UtilityProject.Application;

namespace BookStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly IShoppingCartApplication _shoppingCartApplication;
        public HomeController(IProductApplication productApplication,
            IShoppingCartApplication shoppingCartApplication)
        {
            _productApplication = productApplication;
            _shoppingCartApplication = shoppingCartApplication;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = _productApplication.GetProductsWithRelation();
            return View(products);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCartViewModel cart = new()
            {
                Product = _productApplication.GetProduct(productId),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCartViewModel cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CreateShoppingCart shoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                Count = cart.Count,
                Price = cart.Price,
                ProductId = cart.ProductId
            };

            var result = _shoppingCartApplication.AddToCart(shoppingCart);
            TempData["success"] = result;

            HttpContext.Session.SetInt32(SD.SessionCart,
                        _shoppingCartApplication.GetShoppingCarts(claim.Value).Count());
            return RedirectToAction(nameof(Index));



        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
