using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Product
{
    public interface IProductApplication
    {
        string Create(CreateProduct command);
        string  Edit(EditProduct command);

        string Delete(int id);  


        EditProduct GetDetails(int id);
        ProductViewModel GetProduct(int id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> GetProductsWithRelation();
    }
}
