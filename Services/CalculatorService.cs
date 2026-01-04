using Speed.Models;
using Speed.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Speed.Services
{
    public interface ICalculatorService
    {
        Task<decimal> CalculateCostAsync(TransportRequest request);
        bool ValidateDates(DateTime loading, DateTime unloading);
    }

    public class CalculatorService : ICalculatorService
    {
        private readonly ApplicationDbContext _context;

        public CalculatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ValidateDates(DateTime loading, DateTime unloading)
        {
            // 1.8 Loading or unloading data and times cannot be in the past
            if (loading < DateTime.Now || unloading < DateTime.Now)
                return false;
            
            if (unloading < loading)
                return false;

            return true;
        }

        public async Task<decimal> CalculateCostAsync(TransportRequest request)
        {
            decimal cost = 0;

            // 1. Fetch Vehicle Price
            if (request.SelectedVehicleId.HasValue)
            {
                var vehicle = await _context.Vehicles.FindAsync(request.SelectedVehicleId.Value);
                if (vehicle != null)
                {
                    // Mock distance for now (e.g., 500km default) since we barely have address logic yet
                    // In a real app, use Google Maps API or similar to get distance from LoadingAddress to UnloadingAddress
                    double distanceKm = 500; 
                    cost += (decimal)(vehicle.PricePerKm * distanceKm);
                }
            }

            // 2. Add Services
            if (request.SelectedServiceIds != null && request.SelectedServiceIds.Any())
            {
                var services = await _context.AdditionalServices
                    .Where(s => request.SelectedServiceIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var service in services)
                {
                    cost += service.Price;
                }
            }

            // Basic logic for payload weight? (Optional implementation detail)
            
            return cost;
        }
    }
}
