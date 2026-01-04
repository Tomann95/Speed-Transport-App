using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Speed.Controllers
{
    [Authorize]
    public class CargoPlannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
