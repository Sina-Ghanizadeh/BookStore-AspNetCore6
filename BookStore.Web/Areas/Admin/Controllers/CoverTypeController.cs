using BookStore.Application.Contracts.CoverType;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class CoverTypeController : Controller
    {
        private readonly ICoverTypeApplication _coverTypeApplication;

        public CoverTypeController(ICoverTypeApplication coverTypeApplication)
        {
            _coverTypeApplication = coverTypeApplication;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverTypeViewModel> coverTypes = _coverTypeApplication.GetCoverTypes();
            return View(coverTypes);

        }

        //Get
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCoverType coverType)
        {
            if (!ModelState.IsValid)
                return View(coverType);

            string result = _coverTypeApplication.Create(coverType);
            

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
            
            var coverType = _coverTypeApplication.GetDetails(id);
            
            if (coverType == null)
            {
                TempData["error"] = "Record Not Found";

                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCoverType coverType)
        {
            if (!ModelState.IsValid)
                return View(coverType);
            string result = _coverTypeApplication.Edit(coverType);

            //TempData["success"] = $"Cover Type '{coverType.Name}' Updated Successfully!";
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
            var result = _coverTypeApplication.Delete(id);
            //TempData["success"] = $"Cover Type '{coverType.Name}' Deleted Successfully!";
            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }
    }
}
