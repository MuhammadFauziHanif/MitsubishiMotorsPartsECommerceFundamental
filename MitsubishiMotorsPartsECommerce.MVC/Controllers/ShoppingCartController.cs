using Microsoft.AspNetCore.Mvc;
using MitsubishiMotorsPartsECommerce.MVC.Models;

namespace MitsubishiMotorsPartsECommerce.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productID, string productName, decimal price, int quantity, string imageUrl)
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var existingItem = cart.FirstOrDefault(item => item.ProductID == productID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new ShoppingCartItem
                {
                    ProductID = productID,
                    ProductName = productName,
                    Price = price,
                    ImageUrl = imageUrl,
                    Quantity = quantity
                });
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productID)
        {
            var cart = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var itemToRemove = cart.FirstOrDefault(item => item.ProductID == productID);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }


    }
}
