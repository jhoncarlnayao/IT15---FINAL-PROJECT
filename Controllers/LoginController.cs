using IT15_FINALPROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IT15_FINALPROJECT.Controllers
{
    public class loginController : Controller
    {
        private readonly ILogger<loginController> _logger;

        public loginController(ILogger<loginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
