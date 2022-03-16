using BookStore.Application.Contracts.Category;
using BookStore.Domain.CategoryAgg;
using UtilityProject.Application;

namespace BookStore.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public string Create(CreateCategory command)
        {
            if (_categoryRepository.IsExists(x => x.Name == command.Name))
                return ApplicationMessages.DuplicatedRecord;


            Category category = new()
            {
                Name = command.Name,
                DisplayOrder = command.DisplayOrder,
                CreatedDateTime = DateTime.Now
            };
            _categoryRepository.Add(category);
            _categoryRepository.Save();

            return ApplicationMessages.Success;

        }

        public string Edit(EditCategory command)
        {
            var category = _categoryRepository.GetFirstOrDefault(c=>c.Id == command.Id);
            if(category == null)
                return ApplicationMessages.RecordNotFound;
            if(_categoryRepository.IsExists(x=>x.Name ==  command.Name))
                return ApplicationMessages.RecordNotFound;
            
            category.Name = command.Name;
            category.DisplayOrder = command.DisplayOrder;

            _categoryRepository.Update(category);
            _categoryRepository.Save();

             return ApplicationMessages.Success;

        }
        public string Delete(int id)
        {
            var cover = _categoryRepository.GetFirstOrDefault(x => x.Id == id);
            if (cover == null)
                return ApplicationMessages.RecordNotFound;

            _categoryRepository.Delete(cover);
            _categoryRepository.Save();
            return ApplicationMessages.Success;
        }

        public List<CategoryViewModel> GetAllCategories()
        {
           return _categoryRepository.GetAllCategories();
        }

        public EditCategory GetDetails(int id)
        {
            return _categoryRepository.GetDetails(id);
        }
    }
}
