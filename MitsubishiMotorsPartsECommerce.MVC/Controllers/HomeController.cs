using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.BO;
using MitsubishiMotorsPartsECommerce.MVC.Models;
using System.Diagnostics;

namespace MitsubishiMotorsPartsECommerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICategoryBLL _categoryBLL;
        private readonly IProductBLL _productBLL;

        public HomeController(ILogger<HomeController> logger, ICategoryBLL categoryBLL, IProductBLL productBLL)
        {
            _logger = logger;
            _categoryBLL = categoryBLL;
            _productBLL = productBLL;
        }

        public IActionResult Index(int? categoryID)
        {
            var categories = _categoryBLL.GetAll();
            ViewBag.Categories = categories;

            IEnumerable<ProductDTO> products;

            if (categoryID.HasValue)
            {
                products = _productBLL.GetProductByCategory(categoryID.Value);
            }
            else
            {
                products = _productBLL.GetAll();
            }

            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
