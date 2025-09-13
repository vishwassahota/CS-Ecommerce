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
    [Authorize (Roles =SD.Role_Admin + "," + SD.Role_Employee)]
    public class CoverTypeController : Controller
    {
       
        private readonly IUnitofWork _unitofWork;
        public CoverTypeController(IUnitofWork unitofWork)
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
            //  var coverTypeList = _unitofWork.CoverType.GetAll();
            //  return Json(new { data = coverTypeList });
            var coverTypeList = _unitofWork.SP_CALL.List<CoverType>(SD.proc_GetCoverTypes);
            return Json(new { data = coverTypeList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coverTypeInDb = _unitofWork.CoverType.Get(id);
            if (coverTypeInDb == null)
                return Json(new { success = false, message = "Something went wrong while delete data !!" });
            _unitofWork.CoverType.Remove(coverTypeInDb);
            _unitofWork.Save();
            return Json(new { success = true, message = "data Delete successfully!!" });
        }
        #endregion
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType);
            coverType = _unitofWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View(coverType);
            if (coverType.Id == 0) _unitofWork.CoverType.Add(coverType);
            else _unitofWork.CoverType.Update(coverType);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
