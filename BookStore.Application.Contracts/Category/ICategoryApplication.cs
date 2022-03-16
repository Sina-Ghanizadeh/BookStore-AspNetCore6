using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Category
{
    public interface ICategoryApplication
    {
        string Create(CreateCategory command);
        string Edit(EditCategory command);
        string Delete(int id);
        EditCategory GetDetails(int id);

        List<CategoryViewModel> GetAllCategories();
    }
}
