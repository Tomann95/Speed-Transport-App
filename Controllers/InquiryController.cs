using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speed.Data;
using Speed.Models;
using System.Threading.Tasks;

namespace Speed.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Public: Show Contact Form
        public IActionResult Create()
        {
            return View();
        }

        // Public: Submit Inquiry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject,Email,Message")] Inquiry inquiry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inquiry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Success));
            }
            return View(inquiry);
        }

        public IActionResult Success()
        {
            return View();
        }

        // Admin: List Inquiries
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Inquiries.OrderByDescending(i => i.CreatedAt).ToListAsync());
        }

        // Admin: Mark as Read (or Details)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null) return NotFound();

            if (!inquiry.IsRead)
            {
                inquiry.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return View(inquiry);
        }
    }
}
