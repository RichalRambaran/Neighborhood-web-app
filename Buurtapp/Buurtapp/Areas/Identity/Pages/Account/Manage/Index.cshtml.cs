using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Buurtapp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Buurtapp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
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
                StatusMessage = "Moderator role has been assigned";
                return Page();
            }
            else
            {
                StatusMessage = "Already moderator";
                return Page();
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
                StatusMessage = "Moderator role has been removed";
                return Page();
            }
            else
            {
                StatusMessage = "No moderator role assigned";
                return Page();
            }
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
