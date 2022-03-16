using BookStore.Application.Contracts.Order.OrderDetail;
using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application
{
    public class OrderDetailApplication : IOrderDetailApplication
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailApplication(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public void Create(OrderDetailViewModel command)
        {
            OrderDetail orderDetail = new()
            {
                Count = command.Count,
                Price = command.Price,
                OrderHeaderId = command.OrderId,
                ProductId = command.ProductId,

            };
            _orderDetailRepository.Add(orderDetail);
            _orderDetailRepository.Save();
        }
    }
}
