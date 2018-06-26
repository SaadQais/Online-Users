using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineUsers.Models;
using OnlineUsers.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineUsers.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            string name = HttpContext.User.Identity.Name;
            var user = UsersUtils.AllUsers.FirstOrDefault(u => u.Name == name);

            if (user != null)
                UsersUtils.AllUsers.Remove(user);

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
    }
}
