using Microsoft.AspNetCore.Identity;
using Speed.Models;
using System;

namespace Speed.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // Seed Roles
            string[] roleNames = { "Admin", "Moderator", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@speed.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin User",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Look for any vehicles.
            if (context.Vehicles.Any())
            {
                return;   // DB has been seeded
            }

            var vehicles = new Vehicle[]
            {
                new Vehicle{Name="Van", Description="Small transporter", CargoCapacityKg=1000, LengthMeters=3.0, WidthMeters=1.8, HeightMeters=1.6, PricePerKm=1.5, GraphicUrl="/images/van.png"},
                new Vehicle{Name="Truck (7.5t)", Description="Medium truck", CargoCapacityKg=3500, LengthMeters=6.0, WidthMeters=2.4, HeightMeters=2.4, PricePerKm=2.5, GraphicUrl="/images/truck_medium.png"},
                new Vehicle{Name="Semi-Trailer", Description="Heavy transport", CargoCapacityKg=24000, LengthMeters=13.6, WidthMeters=2.45, HeightMeters=2.7, PricePerKm=4.0, GraphicUrl="/images/semi.png"}
            };
            context.Vehicles.AddRange(vehicles);

            var services = new AdditionalService[]
            {
                new AdditionalService{Name="Express Delivery", Description="Guaranteed next-day delivery", Price=50.0m},
                new AdditionalService{Name="Cargo Insurance", Description="Full coverage", Price=25.0m},
                new AdditionalService{Name="Lift Gate", Description="For locations without a dock", Price=15.0m}
            };
            context.AdditionalServices.AddRange(services);
            context.SaveChanges();
        }
    }
}
