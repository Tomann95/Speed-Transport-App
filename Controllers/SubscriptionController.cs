using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Speed.Models;
using System;
using System.Threading.Tasks;

namespace Speed.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SubscriptionController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string type)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (type == "Quarterly")
            {
                user.SubscriptionType = "Quarterly";
                user.SubscriptionExpiry = DateTime.Now.AddMonths(3);
            }
            else if (type == "Annual")
            {
                user.SubscriptionType = "Annual";
                user.SubscriptionExpiry = DateTime.Now.AddYears(1);
            }

            // In a real app, verify Payment here (Integration 3.3)
            
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
