using Microsoft.AspNetCore.Mvc;
using Speed.Models;
using Speed.Services;
using Speed.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Speed.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;
        private readonly ApplicationDbContext _context;

        public CalculatorController(ICalculatorService calculatorService, ApplicationDbContext context)
        {
            _calculatorService = calculatorService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
            ViewBag.Services = await _context.AdditionalServices.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calculate(TransportRequest request)
        {
            if (!_calculatorService.ValidateDates(request.LoadingDate, request.UnloadingDate))
            {
                ModelState.AddModelError(string.Empty, "Loading/Unloading dates cannot be in the past, and Unloading must be after Loading.");
            }

            if (ModelState.IsValid)
            {
                var cost = await _calculatorService.CalculateCostAsync(request);
                request.TotalCost = cost;
                
                // Return partial view for interactive preview ideally, or just setup viewbag again and return View
                ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
                ViewBag.Services = await _context.AdditionalServices.ToListAsync();
                ViewBag.EstimatedCost = cost;
                return View("Index", request);
            }

            ViewBag.Vehicles = await _context.Vehicles.ToListAsync();
            ViewBag.Services = await _context.AdditionalServices.ToListAsync();
            return View("Index", request);
        }
    }
}
