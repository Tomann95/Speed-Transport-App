using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speed.Data;
using Speed.Models;
using System.Threading.Tasks;

namespace Speed.Controllers
{
    // 2.0 Ability to log in as an administrator
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 2.1 Ability to edit prices for the transport calculator and additional services.
        
        // --- Vehicle Management ---
        public async Task<IActionResult> Vehicles()
        {
            return View(await _context.Vehicles.ToListAsync());
        }

        public async Task<IActionResult> EditVehicle(int? id)
        {
            if (id == null) return NotFound();

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null) return NotFound();
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicle(int id, [Bind("Id,Name,Description,CargoCapacityKg,LengthMeters,WidthMeters,HeightMeters,PricePerKm,GraphicUrl")] Vehicle vehicle)
        {
            if (id != vehicle.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Vehicles));
            }
            return View(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

        // --- Additional Services Management ---
        public async Task<IActionResult> Services()
        {
            return View(await _context.AdditionalServices.ToListAsync());
        }

        public async Task<IActionResult> EditService(int? id)
        {
            if (id == null) return NotFound();

            var service = await _context.AdditionalServices.FindAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditService(int id, [Bind("Id,Name,Description,Price")] AdditionalService service)
        {
            if (id != service.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Services));
            }
            return View(service);
        }

        private bool ServiceExists(int id)
        {
            return _context.AdditionalServices.Any(e => e.Id == id);
        }
    }
}
