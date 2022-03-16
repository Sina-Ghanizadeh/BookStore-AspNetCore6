using BookStore.Application.Contracts.Product;
using BookStore.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityProject.Application;

namespace BookStore.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public string Create(CreateProduct command)
        {
            if (_productRepository.IsExists(x => x.ISBN == command.ISBN))
                return ApplicationMessages.DuplicatedRecord;

            Product product = new()
            {
                ISBN = command.ISBN,
                Title = command.Title,
                Description = command.Description,
                Author = command.Author,
                CoverTypeId = command.CoverTypeId,
                CategoryId = command.CategoryId,
                Price = command.Price,
                ListPrice = command.ListPrice,
                Price100 = command.Price100,
                Price50 = command.Price50,
            };

            var path = $"{command.ISBN}";
            var picturePath = _fileUploader.Upload(command.Image, path);
            product.ImageUrl = picturePath;
            _productRepository.Add(product);
            _productRepository.Save();
            return ApplicationMessages.Success;
        }

        public string Delete(int id)
        {
            var product = _productRepository.GetFirstOrDefault(x => x.Id == id);
            if (product == null)
                return ApplicationMessages.RecordNotFound;

            //Delete Image Of Product
            _fileUploader.Delete(product.ImageUrl);
            _productRepository.Delete(product);
            _productRepository.Save();
            return ApplicationMessages.Success;
        }


        public string Edit(EditProduct command)
        {
            var product = _productRepository.GetFirstOrDefault(c => c.Id == command.Id);
            if (product == null)
                return ApplicationMessages.RecordNotFound;

            product.ISBN = command.ISBN;
            product.Description = command.Description;
            product.Title = command.Title;
            product.CategoryId = command.CategoryId;
            product.Price = command.Price;
            product.Price100 = command.Price100;
            product.Price50 = command.Price50;
            product.Author = command.Author;
            product.CoverTypeId = command.CoverTypeId;

            var path = $"{command.ISBN}";
            var picturePath = _fileUploader.Upload(command.Image, path, product.ImageUrl);
            product.ImageUrl = picturePath;

            _productRepository.Update(product);
            _productRepository.Save();

            return ApplicationMessages.Success;
        }

        public EditProduct GetDetails(int id)
        {
            return _productRepository.GetDetails(id);
        }

        public ProductViewModel GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> GetProductsWithRelation()
        {
            return _productRepository.GetProductsWithRelation();
        }
    }
}
