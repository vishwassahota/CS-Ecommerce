using Ecomm_Project_1030.DataAccess.Repository.IRepository;
using Ecomm_Project_1030.Models;
using Ecomm_Project_1030.Models.ViewModels;
using Ecomm_Project_1030.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecomm_Project_1030.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    { 
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork;

        public HomeController(ILogger<HomeController> logger,IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim !=null)
            {
                var count = _unitofWork.ShoppingCart.GetAll(sp => sp.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount,count);
            }
            var productList = _unitofWork.Product.GetAll(includeProperties:"Category,CoverType");
            return View(productList);
        }
        public IActionResult Details(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitofWork.ShoppingCart.GetAll(sp => sp.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.Ss_CartSessionCount, count);
            }
            var productInDb = _unitofWork.Product.FirstorDefault(p => p.Id == id,
                includeProperties: "Category,CoverType");
            if (productInDb == null) return NotFound();
            var shoppingCart = new ShoppingCart()
            {
                Product = productInDb,
                ProductId = productInDb.Id
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                shoppingCart.ApplicationUserId = claim.Value;

                var shoppingCartFromDb = _unitofWork.ShoppingCart.FirstorDefault
                    (sp => sp.ApplicationUserId == claim.Value && sp.ProductId == shoppingCart.ProductId);
                if (shoppingCartFromDb == null)
                {
                    _unitofWork.ShoppingCart.Add(shoppingCart);
                }
                else
                {
                    shoppingCartFromDb.Count += shoppingCart.Count;
                }
                _unitofWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var productInDb = _unitofWork.Product.FirstorDefault(p => p.Id == shoppingCart.Id,
                includeProperties: "Category,CoverType");
                if (productInDb == null) return NotFound();
                var shoppingCartEdit = new ShoppingCart()
                {
                    Product = productInDb,
                    ProductId = productInDb.Id
                };
                return View(shoppingCart);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
