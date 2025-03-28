using IT15_FINALPROJECT.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Diagnostics;

namespace IT15_FINALPROJECT.Controllers
{
    public class LoginController : Controller
    {
        private readonly TenantContext _context;

        public LoginController(TenantContext context)
        {
            _context = context;
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Login(Tenant tenant)
        {
            var user = _context.Tenants.FirstOrDefault(t => t.Email == tenant.Email && t.Password == tenant.Password);

            if (user != null)
            {
                TempData["SuccessMessage"] = "Login successful! Redirecting...";
                return RedirectToAction("Dashboard", "Home"); // Redirect to your dashboard or home page
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }


        public IActionResult Register()
        {
            return View(new Tenant()); // Pass an empty Tenant model
        }

        [HttpPost]
        public IActionResult Register(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Tenants.Add(tenant);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Register");
            }

            return View(tenant);
        }
    }
}
