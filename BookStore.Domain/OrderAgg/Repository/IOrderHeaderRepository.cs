using BookStore.Application.Contracts.Order.OrderHeader;

namespace BookStore.Domain.OrderAgg.Repository
{
    public interface IOrderHeaderRepository : IRepositoryBase<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        OrderHeaderViewModel GetOrderHeader(int id);
    }
}
