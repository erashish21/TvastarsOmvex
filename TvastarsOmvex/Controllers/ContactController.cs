using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TvastarsOmvex.Data;
using TvastarsOmvex.Models;

namespace TvastarsOmvex.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enquiries = await _context.Enquiries
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();

            return View(enquiries);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                enquiry.CreatedAt = DateTime.Now;
                _context.Enquiries.Add(enquiry);
                _context.SaveChanges();
                ViewBag.Success = true;
            }
            return View();
        }
    }
}
