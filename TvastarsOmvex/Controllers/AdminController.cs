using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvex.Models;

namespace TvastarsOmvex.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Admin Dashboard (View all products)
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        // ✅ Create Product
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                // Save uploaded image
                if (imageFile != null)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var path = Path.Combine("wwwroot/images/products", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImagePath = "/images/products" + fileName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // ✅ Edit Product
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(product);
            }

            var dbProduct = await _context.Products.FindAsync(product.Id);
            if (dbProduct == null) return NotFound();

            dbProduct.Name = product.Name;
            dbProduct.ShortDescription = product.ShortDescription;
            dbProduct.LongDescription = product.LongDescription;
            dbProduct.CategoryId = product.CategoryId;

            if (imageFile != null)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var path = Path.Combine("wwwroot/images/products", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                dbProduct.ImagePath = "/images/products/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ Delete Product
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ManageServices()
        {
            var services = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category!.Name == "FLOWCODE" || p.Category!.Name == "QUADPHASE")
                .ToListAsync();

            return View(services); // looks for Views/Admin/ManageServices.cshtml
        }
        public async Task<IActionResult> AdminDashboard()
        {
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.TotalEnquiries = await _context.Enquiries.CountAsync();
            ViewBag.TotalProjects = await _context.Projects.CountAsync();
            return View();
        }

    }
}
