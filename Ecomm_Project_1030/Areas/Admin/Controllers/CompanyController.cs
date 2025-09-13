using Ecomm_Project_1030.DataAccess.Repository.IRepository;
using Ecomm_Project_1030.Models;
using Ecomm_Project_1030.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1030.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CompanyController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        #region APIs
        public IActionResult GetAll()
        {
            var companyList = _unitofWork.Company.GetAll();
            return Json(new { data = companyList });
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id==null) return View(company);
            company = _unitofWork.Company.Get(id.GetValueOrDefault());
            return View(company);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (company==null) return NotFound();
            if (!ModelState.IsValid) return View(company);
            if (company.Id == 0)
                _unitofWork.Company.Add(company);
            else
            _unitofWork.Company.Update(company);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
