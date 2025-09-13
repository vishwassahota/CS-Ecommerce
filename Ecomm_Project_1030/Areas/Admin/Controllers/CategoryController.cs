using Ecomm_Project_1030.DataAccess.Repository;
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
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoryList = _unitofWork.Category.GetAll();
            return Json(new { data = categoryList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryInDb = _unitofWork.Category.Get(id);
            if(categoryInDb==null)
            return Json(new { success=false,message="Something went wrong while delete data !!"});
            _unitofWork.Category.Remove(categoryInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "data Delete successfully!!" });
        }
        #endregion

        public IActionResult Upsert(int? id)
        {
            Category Category = new Category();
            if (id == null) return View(Category);
            Category = _unitofWork.Category.Get(id.GetValueOrDefault());
            if (Category == null) return NotFound();
            return View(Category);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (category == null) return NotFound();
            if (!ModelState.IsValid) return View();
            if (category.Id == 0) _unitofWork.Category.Add(category);
            else
            {
                if (category == null) return NotFound();
                _unitofWork.Category.Update(category);
            }
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
