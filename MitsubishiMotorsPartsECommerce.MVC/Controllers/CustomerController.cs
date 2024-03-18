using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using System.Text.Json;

namespace MitsubishiMotorsPartsECommerce.MVC.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerBLL _customerBLL;

        public CustomerController(ICustomerBLL customerBLL)
        {
            _customerBLL = customerBLL;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            //if (TempData["Message"] != null)
            //{
            //    ViewBag.Message = TempData["Message"];
            //}

            return View();
        }

        [HttpPost]
        public IActionResult Login(CustomerLoginDTO customerLoginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var customerDto = _customerBLL.Login(customerLoginDTO);
                //simpan customername ke session
                HttpContext.Session.Set("Customer", customerDto);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomerCreateDTO customerCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _customerBLL.Insert(customerCreateDto);
                ViewBag.Message = @"<div class='alert alert-success'><strong>Success!&nbsp;</strong>Registration process successfully !</div>";

            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Customer");
            return RedirectToAction("Login");
        }
    }
}
