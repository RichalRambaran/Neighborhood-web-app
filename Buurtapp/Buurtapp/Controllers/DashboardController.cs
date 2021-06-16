using Buurtapp.Areas.Identity.Data;
using Buurtapp.Data;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Buurtapp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ILogger<DashboardController> logger, UserContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // GET
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult NewPost()
        {
            return View();
        }

        public IActionResult LikedPosts()
        {
            List<Post> result = GetLikedPosts();
            return View(result);
        }

        private List<Post> GetLikedPosts()
        {
            string userId = _userManager.GetUserId(User);
            List<int> likedPostsIds = _context.UserLikesPosts.Where(p => p.UserId == userId).Select(p => p.PostId).ToList();
            List<Post> likedPosts = new List<Post>();
            likedPosts = _context.Posts.Where(p => likedPostsIds.Contains(p.PostId)).ToList();
            return likedPosts;
        }
    }
}