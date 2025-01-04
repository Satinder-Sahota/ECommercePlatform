using ECommercePlatform.Data;
using ECommercePlatform.Models;
using ECommercePlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;

namespace ECommercePlatform.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly ECommerceDbContext _context;
        public CartController(CartService cartService, ECommerceDbContext context)
        {
            _cartService=cartService;
            _context=context;
        }
        public IActionResult Index()
        {
            return View(_cartService.GetCartItems());
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price, int quantity = 1)
        {
            _cartService.AddToCart(productId, productName, price, quantity);
            // Store the quantity in session
            HttpContext.Session.SetInt32($"Quantity_{productId}", quantity);
            TempData["Message"] = "Item added to cart successfully!";
            return RedirectToAction("Index", "Product"); // Redirect back to the product list
        }
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index","Cart");
        }
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var cartItems = _cartService.GetCartItems();
            if (!cartItems.Any())
                return RedirectToAction("Index","Cart");

            var order = new Order
            {
                UserId = User.Identity.Name,
                OrderDate = DateTime.Now,
                TotalAmount = _cartService.GetTotalAmount(),
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId=item.ProductId,
                    Quantity=item.Quantity,
                    Price=item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            _cartService.GetCartItems().ToList().ForEach(item => _cartService.RemoveFromCart(item.ProductId));
            return RedirectToAction("OrderConfirmation", new {orderId=order.Id});
        }
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _context.Orders.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product).FirstOrDefault(o => o.Id==orderId);
            return View(order);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            if (quantity < 1)
                quantity = 1; // Prevent invalid quantities

            _cartService.UpdateCartItem(productId, quantity);

            return RedirectToAction("Index", "Cart");
        }
    }
}
