namespace BookStore.Domain.OrderAgg.Repository
{
    public interface IOrderDetailRepository : IRepositoryBase<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
