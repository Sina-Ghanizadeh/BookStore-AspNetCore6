using BookStore.Application.Contracts.Order.OrderDetail;
using BookStore.Application.Contracts.Order.OrderHeader;
using BookStore.Application.Contracts.Order.ShoppingCart;
using BookStore.Application.Contracts.Product;
using BookStore.Application.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using UtilityProject.Application;

namespace BookStore.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderDetailApplication _orderDetailApplication;
        private readonly IOrderHeaderApplication _orderHeaderApplication;
        private readonly IUserApplication _userApplication;
        private readonly IShoppingCartApplication _shoppingCartApplication;
        private readonly IProductApplication _productApplication;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        public ShoppingCartDetailViewModel ShoppingCart { get; set; }

        public CartController(
            IEmailSender emailSender,
            IShoppingCartApplication shoppingCartApplication,
            IProductApplication productApplication,
            IUserApplication userApplication,
            IOrderHeaderApplication orderHeaderApplication,
            IOrderDetailApplication orderDetailApplication)
        {

            _emailSender = emailSender;
            _shoppingCartApplication = shoppingCartApplication;
            _productApplication = productApplication;
            _userApplication = userApplication;
            _orderHeaderApplication = orderHeaderApplication;
            _orderDetailApplication = orderDetailApplication;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                CartItems = _shoppingCartApplication.GetShoppingCarts(claim.Value),
                OrderHeader = new()
            };
            foreach (var item in ShoppingCart.CartItems)
            {
                item.Product = _productApplication.GetProduct(item.ProductId);
                item.Price = GetPriceBasedOnQuantity(item.Count,
                    item.Product.Price,
                    item.Product.Price50,
                    item.Product.Price100);

                ShoppingCart.OrderHeader.OrderTotal += item.Count * item.Price;
            }

            return View(ShoppingCart);
        }

        public IActionResult Plus(int cartId)
        {

            string result = _shoppingCartApplication.IncrementCount(cartId, 1);

            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {

            string result = _shoppingCartApplication.DecrementCount(cartId, 1);

            FillSession();

            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {

            var result = _shoppingCartApplication.Remove(cartId);
            FillSession();
            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else if (quantity <= 100)
            {
                return price50;
            }
            else
            {
                return price100;
            }
        }
        private void FillSession()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            HttpContext.Session.SetInt32(SD.SessionCart, _shoppingCartApplication.GetShoppingCarts(claim.Value).Count());
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new()
            {
                CartItems = _shoppingCartApplication.GetShoppingCarts(claim.Value),
                OrderHeader = new()
            };
            ShoppingCart.OrderHeader.ApplicationUser = _userApplication.GetApplicationUser(claim.Value);
            ShoppingCart.OrderHeader.Name = ShoppingCart.OrderHeader.ApplicationUser.Name;
            ShoppingCart.OrderHeader.PhoneNumber = ShoppingCart.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCart.OrderHeader.StreetAddress = ShoppingCart.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCart.OrderHeader.City = ShoppingCart.OrderHeader.ApplicationUser.City;
            ShoppingCart.OrderHeader.State = ShoppingCart.OrderHeader.ApplicationUser.State;
            ShoppingCart.OrderHeader.PostalCode = ShoppingCart.OrderHeader.ApplicationUser.PostalCode;


            foreach (var item in ShoppingCart.CartItems)
            {
                item.Product = _productApplication.GetProduct(item.ProductId);
                item.Price = GetPriceBasedOnQuantity(item.Count,
                    item.Product.Price,
                    item.Product.Price50,
                    item.Product.Price100);

                ShoppingCart.OrderHeader.OrderTotal += item.Count * item.Price;
            }

            return View(ShoppingCart);
        }



        [ActionName("Summary")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart.CartItems = _shoppingCartApplication.GetShoppingCarts(claim.Value);



            ShoppingCart.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCart.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var item in ShoppingCart.CartItems)
            {
                item.Product = _productApplication.GetProduct(item.ProductId);
                item.Price = GetPriceBasedOnQuantity(item.Count,
                    item.Product.Price,
                    item.Product.Price50,
                    item.Product.Price100);

                ShoppingCart.OrderHeader.OrderTotal += item.Count * item.Price;
            }

            var applicationUser =
               _userApplication.GetApplicationUser(claim.Value);

            if (applicationUser.CompanyId == 0)
            {
                ShoppingCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCart.OrderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
                ShoppingCart.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCart.OrderHeader.OrderStatus = SD.StatusApproved;
            }


            var headerId = _orderHeaderApplication.Create(ShoppingCart.OrderHeader);
            ShoppingCart.OrderHeader.Id = headerId;
            foreach (var item in ShoppingCart.CartItems)
            {
                var orderDetail = new OrderDetailViewModel
                {

                    ProductId = item.ProductId,
                    OrderId = ShoppingCart.OrderHeader.Id,
                    Price = item.Price,
                    Count = item.Count

                };
                _orderDetailApplication.Create(orderDetail);

            }


            if (applicationUser.CompanyId == 0)
            {
                //Stripe Settings For Payment
                var domain = "https://localhost:17809/";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card", },
                    LineItems = new List<SessionLineItemOptions>()
                    ,
                    Mode = "payment",
                    SuccessUrl = domain + $"cart/OrderConfirmation?id={ShoppingCart.OrderHeader.Id}",
                    CancelUrl = domain + $"cart/index",
                };

                foreach (var item in ShoppingCart.CartItems)
                {

                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Title,
                                Description = item.Product.Description
                            },

                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);
                _orderHeaderApplication.UpdateStripePaymentId(ShoppingCart.OrderHeader.Id,
                    session.Id, session.PaymentIntentId);
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            else
            {
                return RedirectToAction(nameof(OrderConfirmation), ShoppingCart.OrderHeader.Id);
            }




        }

        public IActionResult OrderConfirmation(int id)
        {


            OrderHeaderViewModel orderHeader = _orderHeaderApplication.GetOrderHeader(id);
            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _orderHeaderApplication.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                }
            }
            _emailSender.SendEmailAsync(orderHeader.UserEmail, "New Order Created",
                "<p>New Order</p>"
                );

            _shoppingCartApplication.DeleteRange(orderHeader.ApplicationUserId);

            HttpContext.Session.Remove(SD.SessionCart);

            return View(id);
        }
    }
}


