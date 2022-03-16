using BookStore.Application.Contracts.Order.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UtilityProject.Application;

namespace BookStore.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IShoppingCartApplication _shoppingCartApplication;

        public ShoppingCartViewComponent(IShoppingCartApplication shoppingCartApplication)
        {
            _shoppingCartApplication = shoppingCartApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {

                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _shoppingCartApplication.GetShoppingCarts(claim.Value).Count());
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));

                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
