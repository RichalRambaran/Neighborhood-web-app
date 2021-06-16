using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Buurtapp.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Web;



namespace Buurtapp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AppUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        
        //Waarom staat ViewBag in t rood? ik heb dit gevonden maar kon er geen raad mee https://stackoverflow.com/questions/10885886/cannot-resolve-symbol-viewbag
        //Zie Login.cshtml regel 65 waar de viewbag in staat(die doet t wel)
        public ViewResult Login()
        {
            //ViewBag.melding = meldingMethode;
            //return View();
            return null;
        }
        public  String meldingMethode()
        {    
            
            return null;
        }
        
        //ik heb een count gemaakt maar ik weet niet waar ik count++ moet zette
        static int count = 0;
        //ViewBag.isTrue = false;
        //private static bool testttt = false;

        private static bool forgotPassword = false;
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ViewData["test"] = 0; //0 is false
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    count++;
                    if (count % 4 ==0)
                    {
                        ViewData["test"] = 1; // 1 is true
                        //testttt = true;
                    }
                    if (count % 3 == 0)
                    {
                        ModelState.AddModelError(string.Empty, "3x fout inlogpogingen, klik op 'Forgot your password?' om uw wachtwoord te veranderen.");
                        forgotPassword = true;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                    return Page();
                    //probeer beide
                }
            }
            else
            {
                // If we got this far, something failed, redisplay form
                return Page();
                count++;
                //probeer beide
            }
        }
    }
}
