using BookStore.Application.Contracts.Order.OrderHeader;
using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.EFCore.Repository
{
    public class OrderHeaderRepository : RepositoryBase<OrderHeader>, IOrderHeaderRepository
    {
        private readonly DatabaseContext _context;

        public OrderHeaderRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public OrderHeaderViewModel GetOrderHeader(int id)
        {
            return _context.OrderHeaders
                .Include(x=>x.ApplicationUser)
                .Select(x => new OrderHeaderViewModel
            {
                Id = id,
                ApplicationUserId = x.ApplicationUserId,
                Carrier = x.Carrier,
                City = x.City,
                Name = x.Name,
                OrderDate = x.OrderDate,
                OrderStatus = x.OrderStatus,
                OrderTotal = x.OrderTotal,
                PaymentDate = x.PaymentDate,
                PaymentDueDate = x.PaymentDueDate,
                PaymentIntentId = x.PaymentIntentId,
                PaymentStatus = x.PaymentStatus,
                PhoneNumber = x.PhoneNumber,
                PostalCode = x.PostalCode,
                SessionId = x.SessionId,
                ShippingDate = x.ShippingDate,
                State = x.State,
                StreetAddress = x.StreetAddress,
                TrackingNumber = x.TrackingNumber,
                UserEmail = x.ApplicationUser.Email
            }).FirstOrDefault(x => x.Id == id);
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.Update(orderHeader);
        }

        

       
    }
}
