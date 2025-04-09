using Microsoft.AspNetCore.Mvc;
using IT15_FINALPROJECT.Model;
using IT15_FINALPROJECT.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT15_FINALPROJECT.Controllers
{
    public class TenantDashboardController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TenantDashboardController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult TenantDashboard()
        {
            return View();
        }

       
    }
    
    }

