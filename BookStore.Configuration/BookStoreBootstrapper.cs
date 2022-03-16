using BookStore.Application;
using BookStore.Application.Contracts.Category;
using BookStore.Application.Contracts.Company;
using BookStore.Application.Contracts.CoverType;
using BookStore.Application.Contracts.Order.OrderDetail;
using BookStore.Application.Contracts.Order.OrderHeader;
using BookStore.Application.Contracts.Order.ShoppingCart;
using BookStore.Application.Contracts.Product;
using BookStore.Application.Contracts.User;
using BookStore.Domain.CategoryAgg;
using BookStore.Domain.CompanyAgg;
using BookStore.Domain.CoverTypeAgg;
using BookStore.Domain.OrderAgg.Repository;
using BookStore.Domain.ProductAgg;
using BookStore.Domain.UserAgg;
using BookStore.EFCore.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Configuration
{
    public class BookStoreBootstrapper
    {
        public static void Configure(IServiceCollection services)
        {
            //Service Registration For CoverType
            services.AddTransient<ICoverTypeApplication, CoverTypeApplication>();
            services.AddTransient<ICoverTypeRepository, CoverTypeRepository>();

            //Service Registration For Category
            services.AddTransient<ICategoryApplication, CategoryApplication>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            //Service Registration For Company
            services.AddTransient<ICompanyApplication, CompanyApplication>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();

            //Service Registration For Product
            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            //Service Registration For ShoppingCart
            services.AddTransient<IShoppingCartApplication, ShoppingCartApplication>();
            services.AddTransient<IShoppingCartRepository , ShoppingCartRepository>();

            //Service Registration For Application User
            services.AddTransient<IUserApplication, UserApplication>();
            services.AddTransient<IApplicationUserRepository , ApplicationUserRepository>();

            //Service Registration For Order Detail
            services.AddTransient<IOrderDetailApplication, OrderDetailApplication>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

            //Service Registration For Order Header
            services.AddTransient<IOrderHeaderApplication, OrderHeaderApplication>();
            services.AddTransient<IOrderHeaderRepository, OrderHeaderRepository>();




        }

    }
}
