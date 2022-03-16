using BookStore.Application.Contracts.Product;

namespace BookStore.Domain.ProductAgg
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        void Update(Product product);
        EditProduct GetDetails(int id);
        ProductViewModel GetProduct(int id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> GetProductsWithRelation();
    }
}
