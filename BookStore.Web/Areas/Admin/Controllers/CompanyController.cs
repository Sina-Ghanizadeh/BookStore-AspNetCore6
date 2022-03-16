using BookStore.Application.Contracts.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtilityProject.Application;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CompanyController : Controller
    {
        private readonly ICompanyApplication _companyApplication;

        public CompanyController(ICompanyApplication companyApplication)
        {
            _companyApplication = companyApplication;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCompany obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }


            string result = _companyApplication.Create(obj);
            //TempData["success"] = $"Company '{obj.Name}' Created Successfully!";
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

            var company = _companyApplication.GetDetails(id);
            if (company == null)
            {
                TempData["error"] = "Record Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCompany obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            string result = _companyApplication.Edit(obj);
            //TempData["success"] = $"Company '{obj.Name}' Created Successfully!";
            TempData["success"] = result;

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            var companies = _companyApplication.GetAllCompanies();

            return Json(new
            {
                data = companies
            });


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Record Not Found" });
            }
            string result = _companyApplication.Delete(id);

            if (result == ApplicationMessages.RecordNotFound)
            {
                return Json(new { success = false, message = result });
            }

            return Json(new { success = true, message = result });



        }
        #endregion
    }



}


