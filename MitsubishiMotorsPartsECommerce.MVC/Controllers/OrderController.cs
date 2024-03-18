using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.BLL;
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.MVC.Models;

namespace MitsubishiMotorsPartsECommerce.MVC.Controllers
{
    public class OrderController : Controller
    {

        private readonly SalesOrderBLL _salesOrderBLL;

        public OrderController(ISalesOrderBLL salesOrderBLL)
        {
            _salesOrderBLL = new SalesOrderBLL();
        }
        public IActionResult Index()
        {
            var customer = HttpContext.Session.Get<CustomerDTO>("Customer");

            if (customer == null)
            {
                TempData["message"] = @"<div class='alert alert-danger'>You must login first to see your order</div>";
                return RedirectToAction("Login", "Customer");
            }

            var SalesOrderHeader = _salesOrderBLL.GetSalesOrderHeaderByCustomerID(customer.CustomerID);

            return View(SalesOrderHeader);
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart");
            var customer = HttpContext.Session.Get<CustomerDTO>("Customer");

            if (customer == null)
            {
                TempData["message"] = @"<div class='alert alert-danger'>You must login first to create order</div>";
                return RedirectToAction("Login", "Customer");
            }

            if (cart == null || cart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var salesOrderCreate = new SalesOrderCreateDTO
            {
                CustomerID = customer.CustomerID,
                LstProd = string.Join(",", cart.Select(item => $"{item.ProductID}-{item.Quantity}"))
            };

            var orderID = _salesOrderBLL.Create(salesOrderCreate);

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
