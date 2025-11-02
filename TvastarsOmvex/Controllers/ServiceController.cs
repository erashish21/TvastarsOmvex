using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvex.Models;

namespace TvastarsOmvex.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ 1. Show all FLOWCODE and QUADPHASE services
        public async Task<IActionResult> Index()
        {
            var flowcode = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name == "FLOWCODE")
                .ToListAsync();

            var quadphase = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name == "QUADPHASE")
                .ToListAsync();

            // Optional: fetch all services that are not in FLOWCODE/QUADPHASE
            var others = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category != null && p.Category.Name != "FLOWCODE" && p.Category.Name != "QUADPHASE")
                .ToListAsync();

            ViewBag.Flowcode = flowcode;
            ViewBag.Quadphase = quadphase;
            ViewBag.Others = others;

            return View();
        }


        // ✅ 2. Service Details
        public async Task<IActionResult> Details(int id)
        {
            var service = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        // ✅ 3. Create Service (GET)
        [HttpGet]
        public IActionResult Create()
        {
            // Only show FLOWCODE and QUADPHASE categories
            ViewBag.Categories = _context.Categories
                .Where(c => c.Name == "FLOWCODE" || c.Name == "QUADPHASE")
                .ToList();
            return View();
        }

        // ✅ 4. Create Service (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product service, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // ✅ Automatically create the upload folder if it doesn’t exist
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "services");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    // ✅ Generate a unique file name to prevent overwriting
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    // ✅ Save uploaded image to the target directory
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    // ✅ Save relative path for displaying in views
                    service.ImagePath = $"/images/services/{fileName}";
                }

                // ✅ Add service to database
                _context.Products.Add(service);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Service added successfully!";
                return RedirectToAction(nameof(Index));
            }

            // ✅ Refill categories for dropdown if validation fails
            ViewBag.Categories = _context.Categories
                .Where(c => c.Name == "FLOWCODE" || c.Name == "QUADPHASE")
                .ToList();

            return View(service);
        }


        // ✅ 5. Edit Service (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _context.Products.FindAsync(id);
            if (service == null)
                return NotFound();

            ViewBag.Categories = _context.Categories
                .Where(c => c.Name == "FLOWCODE" || c.Name == "QUADPHASE")
                .ToList();
            return View(service);
        }

        // ✅ 6. Edit Service (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product service, IFormFile? imageFile)
        {
            var dbService = await _context.Products.FindAsync(service.Id);
            if (dbService == null)
                return NotFound();

            dbService.Name = service.Name;
            dbService.ShortDescription = service.ShortDescription;
            dbService.LongDescription = service.LongDescription;
            dbService.CategoryId = service.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var folderPath = Path.Combine("wwwroot", "images", "services");
                Directory.CreateDirectory(folderPath);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                dbService.ImagePath = "/images/services/" + fileName;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Service updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ✅ 7. Delete Service
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Products.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Products.Remove(service);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Service deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
