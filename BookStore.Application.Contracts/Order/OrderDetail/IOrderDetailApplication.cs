using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Order.OrderDetail
{
    public interface IOrderDetailApplication
    {
        void Create(OrderDetailViewModel command);
    }
}
