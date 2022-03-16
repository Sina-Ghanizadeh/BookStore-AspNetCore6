using BookStore.Application.Contracts.Category;
using BookStore.Domain.CategoryAgg;
using UtilityProject.Application;

namespace BookStore.EFCore.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public List<CategoryViewModel> GetAllCategories()
        {
            return _context.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                CreatedDateTime = c.CreatedDateTime.ToFarsi(),
                DisplayOrder = c.DisplayOrder,
            }).ToList();
        }

        public EditCategory GetDetails(int id)
        {
            return _context.Categories.Select(c => new EditCategory
            {
                Id = c.Id,
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
            }).FirstOrDefault(x => x.Id == id);
        }

        public void Update(Category category)
        {
            _context.Update(category);
        }
    }
}
