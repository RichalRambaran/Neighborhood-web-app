using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;
using Buurtapp.Areas.Identity.Data;
using Buurtapp.Data;

namespace Buurtapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            UserContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> GetModeratorRole()
        {
            AppUser loggedInUser = await _userManager.GetUserAsync(User);
            await _roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
            bool alreadyIsModerator = await _userManager.IsInRoleAsync(loggedInUser, "Moderator");
            if (alreadyIsModerator == false)
            {
                await _userManager.AddToRoleAsync(loggedInUser, "Moderator");
                await _signInManager.RefreshSignInAsync(loggedInUser);
                //StatusMessage = "Moderator role has been assigned";
                return View(nameof(Index));
            }
            else
            {
                //StatusMessage = "Already moderator";
                return View(nameof(Index));
            }
        }

        public async Task<IActionResult> RemoveModeratorRole()
        {
            AppUser loggedInUser = await _userManager.GetUserAsync(User);
            bool hasModeratorRole = await _userManager.IsInRoleAsync(loggedInUser, "Moderator");
            if (hasModeratorRole == true)
            {
                await _userManager.RemoveFromRoleAsync(loggedInUser, "Moderator");
                await _signInManager.RefreshSignInAsync(loggedInUser);
                //StatusMessage = "Moderator role has been removed";
                return View(nameof(Index));
            }
            else
            {
                //StatusMessage = "No moderator role assigned";
                return View(nameof(Index));
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
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