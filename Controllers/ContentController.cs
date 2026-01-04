using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speed.Data;
using Speed.Models;
using System.Threading.Tasks;

namespace Speed.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.PageContents.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageName,SectionKey,Content")] PageContent pageContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageContent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pageContent = await _context.PageContents.FindAsync(id);
            if (pageContent == null) return NotFound();
            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PageName,SectionKey,Content")] PageContent pageContent)
        {
            if (id != pageContent.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PageContents.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pageContent);
        }
    }
}
