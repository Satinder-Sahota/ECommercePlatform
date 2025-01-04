using ECommercePlatform.Data;
using ECommercePlatform.Models;
using ECommercePlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ECommerceDbContext _context;
        private readonly CartService _cartService;
        public ProductController(ECommerceDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        // Allow all authenticated users to access the Index action
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p=>p.Category).ToListAsync();
            foreach (var product in products)
            {
                int? quantity = HttpContext.Session.GetInt32($"Quantity_{product.Id}");
                ViewData[$"Quantity_{product.Id}"] = quantity ?? 1; // Default to 1 if not found
            }
            ViewBag.CartItemCount = _cartService.GetCartItems().Sum(item => item.Quantity);
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product =  await _context.Products.Include(p=>p.Category)
                .FirstOrDefaultAsync(p=>p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories,"Id","Name");
            ViewData["IncludeValidationScripts"] = true;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["IncludeValidationScripts"] = true;
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["IncludeValidationScripts"] = true;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["IncludeValidationScripts"] = true;
            return View(product);
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
