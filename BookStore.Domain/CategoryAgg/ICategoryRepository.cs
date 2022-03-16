using BookStore.Application.Contracts.Category;

namespace BookStore.Domain.CategoryAgg
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        void Update(Category category);
        EditCategory GetDetails(int id);

        List<CategoryViewModel> GetAllCategories();
    }
}
