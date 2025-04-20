using IT15_FINALPROJECT.Model;
using IT15_FINALPROJECT.Services; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace IT15_FINALPROJECT.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoginController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

      

        [HttpPost]
        public IActionResult Login(Tenant loginData)
        {
            var admin = _context.AdminAccount
                .FirstOrDefault(a => a.Email == loginData.Email && a.Password == loginData.Password);

            if (admin != null)
            {
                var verificationCode = new Random().Next(1000, 9999).ToString();
                TempData["AdminEmail"] = admin.Email;
                TempData["VerificationCode"] = verificationCode;
                TempData["ShowPopup"] = "True";

                SendVerificationEmail(admin.Email, verificationCode);

                return RedirectToAction("Login");
            }

            var tenant = _context.Tenants
                .FirstOrDefault(t => t.Email == loginData.Email && t.Password == loginData.Password);

            if (tenant != null)
            {
               
                HttpContext.Session.SetString("TenantEmail", tenant.Email);

                TempData["SuccessMessage"] = "Tenant login successful!";
                return RedirectToAction("TenantDashboard", "TenantDashboard");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        private void SendVerificationEmail(string toEmail, string code)
        {
            var fromAddress = new System.Net.Mail.MailAddress("jhoncarlnayaoxx@gmail.com", "Your System");
            var toAddress = new System.Net.Mail.MailAddress(toEmail);
            const string fromPassword = "eprj upyb ykuv bjzp";
            const string subject = "Your Verification Code";
            string body = $"Your verification code is: {code}";

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public IActionResult Register()
        {
            return View(new Tenant());
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
