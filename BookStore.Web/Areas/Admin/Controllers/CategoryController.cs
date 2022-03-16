using BookStore.Application.Contracts.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = _categoryApplication.GetAllCategories();
            return View(categories);

        }

        //Get
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategory category)
        {
            if (!ModelState.IsValid)
                return View(category);

            string result = _categoryApplication.Create(category);

            //TempData["success"] = "Category created Successfully!";
            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }

        //Get
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            var category = _categoryApplication.GetDetails(id);
            if (category == null)
            {
                TempData["error"] = "Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategory category)
        {
            if (!ModelState.IsValid)
                return View(category);
            string result = _categoryApplication.Edit(category);

            //TempData["success"] = $"Category '{category.Name}' Updated Successfully!";
            TempData["success"] = result;
            return RedirectToAction(nameof(Index));
        }
        //Get
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Record Not Found";
                return RedirectToAction(nameof(Index));
            }

            string result = _categoryApplication.Delete(id);
            //TempData["success"] = $"Category '{category.Name}' Deleted Successfully!";
            TempData["success"] = result;
            return RedirectToAction(nameof(Index));
        }
    }
}
