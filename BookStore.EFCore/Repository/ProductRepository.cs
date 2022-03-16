using BookStore.Application.Contracts.Product;
using BookStore.Domain.ProductAgg;
using Microsoft.EntityFrameworkCore;

namespace BookStore.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(int id)
        {
            return _context.Products.Select(p => new EditProduct
            {

                Id = p.Id,
                Author = p.Author,
                CategoryId = p.CategoryId,
                CoverTypeId = p.CoverTypeId,
                Description = p.Description,
                ISBN = p.ISBN,
                Title = p.Title,
                ListPrice = p.ListPrice,
                Price = p.Price,
                Price100 = p.Price100,
                Price50 = p.Price50,
                ImageUrl = p.ImageUrl

            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductViewModel GetProduct(int id)
        {
            return _context.Products
                .Include(x=>x.Category)
                .Include(x=>x.CoverType)
                .Select(p => new ProductViewModel
            {

                    Id = p.Id,
                    Title = p.Title,
                    ISBN = p.ISBN,
                    ImageUrl = p.ImageUrl,
                    CoverType = p.CoverType.Name,
                    Author = p.Author,
                    Category = p.Category.Name,
                    CategoryId = p.CategoryId,
                    CoverTypeId = p.CoverTypeId,
                    Description = p.Description,
                    ListPrice = p.ListPrice,
                    Price50 = p.Price50,
                    Price = p.Price,
                    Price100 = p.Price100,

                }).FirstOrDefault(p=>p.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(p => new ProductViewModel {

                Id = p.Id,
                Author = p.Author,
                CategoryId = p.CategoryId,
                CoverTypeId = p.CoverTypeId,
                Description = p.Description,
                ISBN = p.ISBN,
                Title = p.Title,
                ListPrice = p.ListPrice,
                Price = p.Price,
                Price100 = p.Price100,
                Price50 = p.Price50
                
            }).ToList();


            }

        public List<ProductViewModel> GetProductsWithRelation()
        {
           var query = _context.Products.Include(x=>x.Category)
                .Include(x=>x.CoverType)
                .Select(p=> new ProductViewModel { 
                    Id=p.Id,
                    Title =p.Title,
                    ISBN=p.ISBN,
                    ImageUrl = p.ImageUrl,
                    CoverType = p.CoverType.Name,
                    Author = p.Author,
                    Category = p.Category.Name,
                    CategoryId=p.CategoryId,
                    CoverTypeId=p.CoverTypeId,
                    Description=p.Description,
                    ListPrice =p.ListPrice,
                    Price50 = p.Price50,
                    Price = p.Price,
                    Price100= p.Price100,
                    
                    });
            return query.OrderByDescending(p => p.Id).AsNoTracking().ToList();
        }

        public void Update(Product product)
        {
            var productFromDb = _context.Products.FirstOrDefault(u => u.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.Description = product.Description;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price100 = product.Price100;
                productFromDb.Price = product.Price;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Author = product.Author;
                productFromDb.CoverTypeId = product.CoverTypeId;
                productFromDb.Price50 = product.Price50;
                if (product.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }

            }
        }
    }
}
