using BookStore.Application.Contracts.Category;
using BookStore.Application.Contracts.CoverType;
using BookStore.Application.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UtilityProject.Application;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IProductApplication _productApplication;
        private readonly ICategoryApplication _categoryApplication;
        private readonly ICoverTypeApplication _coverTypeApplication;
        public ProductController(IProductApplication productApplication,
            ICategoryApplication categoryApplication,
            ICoverTypeApplication coverTypeApplication)
        {
            _productApplication = productApplication;
            _categoryApplication = categoryApplication;
            _coverTypeApplication = coverTypeApplication;
        }

        public IActionResult Index()
        {

            return View();

        }


        //Get
        public IActionResult Create()
        {
            CreateProduct product = new()
            {
                CategoryList = _categoryApplication.GetAllCategories().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()

                }),
                CoverTypeList = _coverTypeApplication.GetCoverTypes().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })

            };

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProduct obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            //string rootPath = _webHostEnvironment.WebRootPath;
            //if (file != null)
            //{
            //    string fileName = Guid.NewGuid().ToString();
            //    var uploads = Path.Combine(rootPath, @"images\Products");
            //    var extension = Path.GetExtension(file.FileName);
            //    if (obj.Product.ImageUrl != null)
            //    {
            //        var oldImage = Path.Combine(rootPath, obj.Product.ImageUrl.TrimStart('\\'));
            //        if (System.IO.File.Exists(oldImage))
            //        {
            //            System.IO.File.Delete(oldImage);
            //        }
            //    }
            //    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            //    {
            //        file.CopyTo(fileStream);
            //    }
            //    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
            //}

            string result = _productApplication.Create(obj);
            //TempData["success"] = $"Product '{obj.Product.Title}' Created Successfully!";
            TempData["success"] = result;






            return RedirectToAction(nameof(Index));
        }
        //Get
        public IActionResult Edit(int id)
        {



            if (id == null || id == 0)
            {


                return RedirectToAction(nameof(Index));
            }

            var product = _productApplication.GetDetails(id);
            if (product == null)
            {
                TempData["error"] = "Record Not Found";
                return RedirectToAction(nameof(Index));
            }

            product.CategoryList = _categoryApplication.GetAllCategories().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            });
            product.CoverTypeList = _coverTypeApplication.GetCoverTypes().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });



            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProduct obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            
            var result = _productApplication.Edit(obj);
            //TempData["success"] = $"Product '{obj.Product.Title}' Updated Successfully!";
            TempData["success"] = result;




            return RedirectToAction(nameof(Index));
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            var products = _productApplication.GetProductsWithRelation();

            return Json(new
            {
                data = products
            });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Record Not Found" });
            }
            var result = _productApplication.Delete(id);

            if (result == ApplicationMessages.RecordNotFound)
            {
                return Json(new { success = false, message = "Record Not Found" });
            }
            //Delete Image
            //var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            //if (System.IO.File.Exists(oldImage))
            //{
            //    System.IO.File.Delete(oldImage);
            //}
            //_unitOfWork.Product.Delete(product);
            //_unitOfWork.Save();
            return Json(new { success = true, message = result });



        }
        #endregion

    }

}
