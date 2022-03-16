using BookStore.Domain.OrderAgg;
using BookStore.Domain.OrderAgg.Repository;

namespace BookStore.EFCore.Repository
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        private readonly DatabaseContext _context;

        public OrderDetailRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.Update(orderDetail);
        }
    }
}
