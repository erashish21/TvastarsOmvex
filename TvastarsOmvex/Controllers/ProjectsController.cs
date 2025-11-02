using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvex.Models;

namespace TvastarsOmvex.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProjectsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 🔹 Public List of Projects
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(projects);
        }

        // 🔹 Public Project Details
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        // ===================== ADMIN SECTION ======================

        // ✅ Admin: Manage Projects (List)
        public async Task<IActionResult> Manage()
        {
            var projects = await _context.Projects
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(projects); // Views/Projects/Manage.cshtml
        }

        // ✅ Admin: Create Project
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var folderPath = Path.Combine(_env.WebRootPath, "images", "projects");
                    Directory.CreateDirectory(folderPath);

                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    project.ImagePath = "/images/projects/" + fileName;
                }

                project.CreatedAt = DateTime.Now;

                _context.Projects.Add(project);
                await _context.SaveChangesAsync();

                TempData["Success"] = "✅ Project added successfully!";
                return RedirectToAction(nameof(Manage));
            }

            return View(project);
        }

        // ✅ Admin: Edit Project
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Project project, IFormFile? imageFile)
        {
            var existing = await _context.Projects.FindAsync(project.Id);
            if (existing == null) return NotFound();

            existing.Title = project.Title;
            existing.ShortDescription = project.ShortDescription;
            existing.LongDescription = project.LongDescription;

            if (imageFile != null && imageFile.Length > 0)
            {
                var folderPath = Path.Combine(_env.WebRootPath, "images", "projects");
                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existing.ImagePath = "/images/projects/" + fileName;
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Project updated successfully!";
            return RedirectToAction(nameof(Manage));
        }

        // ✅ Admin: Delete Project
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            TempData["Success"] = "🗑️ Project deleted successfully!";
            return RedirectToAction(nameof(Manage));
        }
    }
}
