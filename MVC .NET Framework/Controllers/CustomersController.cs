using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.NET_Framework.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerBLL customerBLL;

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var userDto = _userBLL.LoginMVC(loginDTO);
                //simpan username ke session
                var userDtoSerialize = JsonSerializer.Serialize(userDto);
                HttpContext.Session.SetString("user", userDtoSerialize);

                TempData["Message"] = "Welcome " + userDto.Username;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = @"<div class='alert alert-danger'><strong>Error!&nbsp;</strong>" + ex.Message + "</div>";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }
    }
}