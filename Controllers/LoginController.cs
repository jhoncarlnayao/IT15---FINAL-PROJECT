using IT15_FINALPROJECT.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IT15_FINALPROJECT.Controllers
{
    public class LoginController : Controller
    {
        

        private readonly TenantContext _tenantContext;
        private readonly AdminContext _adminContext;

        public LoginController(TenantContext tenantContext, AdminContext adminContext)
        {
            _tenantContext = tenantContext;
            _adminContext = adminContext;
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
            var admin = _adminContext.AdminAccount
                .FirstOrDefault(a => a.Email == loginData.Email && a.Password == loginData.Password);

            if (admin != null)
            {
                // Generate a 4-digit verification code
                var verificationCode = new Random().Next(1000, 9999).ToString();
                TempData["AdminEmail"] = admin.Email;
                TempData["VerificationCode"] = verificationCode;
                TempData["ShowPopup"] = "True";


                // Send email
                SendVerificationEmail(admin.Email, verificationCode);

                // Redirect to the same login page to trigger the popup
                return RedirectToAction("Login");
            }

            var tenant = _tenantContext.Tenants
                .FirstOrDefault(t => t.Email == loginData.Email && t.Password == loginData.Password);

            if (tenant != null)
            {
                TempData["SuccessMessage"] = "Tenant login successful!";
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }


        private void SendVerificationEmail(string toEmail, string code)
        {
            var fromAddress = new System.Net.Mail.MailAddress("jhoncarlnayaoxx@gmail.com", "Your System");
            var toAddress = new System.Net.Mail.MailAddress(toEmail);
            const string fromPassword = "eprj upyb ykuv bjzp"; // Use app password for Gmail
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
                _tenantContext.Tenants.Add(tenant);
                _tenantContext.SaveChanges();
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Register");
            }

            return View(tenant);
        }
    }
}
