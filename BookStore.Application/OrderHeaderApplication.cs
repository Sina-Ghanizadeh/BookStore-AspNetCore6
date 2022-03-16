using BookStore.Application.Contracts.Order.OrderHeader;
using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application
{
    public class OrderHeaderApplication : IOrderHeaderApplication
    {
        private readonly IOrderHeaderRepository _orderHeaderRepository;

        public OrderHeaderApplication(IOrderHeaderRepository orderHeaderRepository)
        {
            _orderHeaderRepository = orderHeaderRepository;
        }

        public int Create(OrderHeaderViewModel command)
        {
            OrderHeader header = new()
            {
                ApplicationUserId = command.ApplicationUserId,
                Carrier = command.Carrier,
                City = command.City,
                Name = command.Name,
                OrderDate = command.OrderDate,
                OrderStatus = command.OrderStatus,
                OrderTotal = command.OrderTotal,
                PaymentDate = command.PaymentDate,
                PaymentDueDate = command.PaymentDueDate,
                PaymentIntentId = command.PaymentIntentId,
                PaymentStatus = command.PaymentStatus,
                PhoneNumber = command.PhoneNumber,
                SessionId = command.SessionId,
                PostalCode = command.PostalCode,
                ShippingDate = command.ShippingDate,
                State = command.State,
                StreetAddress = command.StreetAddress,
                TrackingNumber = command.TrackingNumber,
            };

            _orderHeaderRepository.Add(header);
            _orderHeaderRepository.Save();

            return header.Id;
        }

        public OrderHeaderViewModel GetOrderHeader(int id)
        {
            return _orderHeaderRepository.GetOrderHeader(id);
        }

        public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = _orderHeaderRepository.GetFirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.SessionId = sessionId;
                orderFromDb.PaymentIntentId = paymentIntentId;
                _orderHeaderRepository.Save();
            }

        }
        public void UpdateStatus(int id, string status, string? paymentStatus = null)
        {
            var orderFromDb = _orderHeaderRepository.GetFirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = status;
                if (paymentStatus != null)
                    orderFromDb.PaymentStatus = paymentStatus;
                _orderHeaderRepository.Save();
            }
        }
    }
}
