using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.OrderHeader
{
    public interface IOrderHeaderApplication
    {
        int Create(OrderHeaderViewModel command);
        void UpdateStatus(int id, string status, string? paymentStatus = null);
        void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
        OrderHeaderViewModel GetOrderHeader(int id);
    }
}
