using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buurtapp.Data;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;
using Buurtapp.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Buurtapp.ViewModels;

namespace Buurtapp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly UserContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public PostController(UserContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnviroment = webHostEnviroment;
        }

        private IQueryable<Post> Sort(string sorteerVolgorde)
        {
            IQueryable<Post> lijst = _context.Posts;
            ViewData["SorteerVolgorde"] = sorteerVolgorde ?? "aflopendDate";
            var posts = _context.Posts.Include(p => p.AppUser).Include(p => p.UserLikesPosts);
            switch (sorteerVolgorde)
            {
                default:
                    lijst = posts.OrderByDescending(p => p.PlaceDate);
                    break;

                case "aflopendDate":
                    lijst = posts.OrderByDescending(p => p.PlaceDate);
                    break;

                case "oplopendDate":
                    lijst = posts.OrderBy(p => p.PlaceDate);
                    break;

                case "aflopendViews":
                    lijst = posts.OrderByDescending(p => p.Views);
                    break;

                case "oplopendViews":
                    lijst = posts.OrderBy(p => p.Views);
                    break;

                case "aflopendLikes":
                    lijst = posts.OrderByDescending(p => p.UserLikesPosts.Count());
                    break;

                case "oplopendLikes":
                    lijst = posts.OrderBy(p => p.UserLikesPosts.Count());
                    break;
            }

            // add likedByMe
            if (lijst != null) {
                var user = _context.Users.Where(p => p.Email == User.Identity.Name).First();
                foreach (var item in lijst)
                {
                    item.likedByMe = item.UserLikesPosts.Any(p => p.UserId == user.Id) ? "liked" : "";
                }
            }

            return lijst;
        }

        private IQueryable<Post> Filter(IQueryable<Post> lijst, string filter)
        {
            ViewData["Filter"] = filter;
            if (!String.IsNullOrEmpty(filter))
                lijst = lijst.Where(s => s.Title.Contains(filter) || s.Content.Contains(filter));
            return lijst;
        }

        // GET: Post
        public async Task<IActionResult> Index(string sorteerVolgorde, string filter, int pagina, int sorteerSoort)
        {
            ViewData["SorteerSoort"] = sorteerSoort;
            ViewData["ingelogdeUserId"] =  _userManager.GetUserId(User);
            return View(await GepagineerdeList<Post>.CreateAsync(Filter(Sort(sorteerVolgorde), filter), pagina, 12));
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AppUser)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            post.Views = post.Views + 1;
            await _context.SaveChangesAsync();

            ViewData["LoggedinUser"] = _userManager.GetUserAsync(User).Result.Id;
            // ViewData["Moderator"] =  _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Moderator");

            DetailsViewModel detailsViewModel = new DetailsViewModel();
            detailsViewModel.Post = post;
            detailsViewModel.Comments = await GetPostComments(post);
            detailsViewModel.Solutions = await GetPostSolutions(post);
            return View(detailsViewModel);
        }

        private async Task<List<Comment>> GetPostComments(Post post)
        {
            List<Comment> comments = await _context.Comments.Where(p => p.PostId == post.PostId).ToListAsync();
            return comments;
        }

        private async Task<List<Solution>> GetPostSolutions(Post post)
        {
            List<Solution> solutions = await _context.Solutions.Where(p => p.PostId == post.PostId).ToListAsync();
            return solutions;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            comment.Post = _context.Posts.Single(p => p.PostId == comment.PostId);
            comment.PlaceDate = DateTime.Today.ToString("dd-MM-y");
            comment.UserId = _userManager.GetUserAsync(User).Result.Id;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSolution([FromBody] Solution solution)
        {
            solution.Post = _context.Posts.Single(p => p.PostId == solution.PostId);
            solution.PlaceDate = DateTime.Today.ToString("dd-MM-y");
            solution.UserId = _userManager.GetUserAsync(User).Result.Id;

            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,UserId,Title,Content,Views,Image,PlaceDate,CategoryId,Anonymous,Archived")] Post post, PostImageFile postImageFile)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadFile(postImageFile);
                post.UserId = _userManager.GetUserId(User);
                post.Image = fileName;
                post.PlaceDate = DateTime.Today.ToString("dd-MM-yyyy");
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            return View(post);
        }

        private string UploadFile(PostImageFile post)
        {
            string fileName = null;
            if (post.Image != null) {
                string uploadDir = Path.Combine(_webHostEnviroment.WebRootPath, "img/uploadedimg");
                fileName = Guid.NewGuid().ToString() + "-" + post.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                    post.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,UserId,Title,Content,Views,Image,PlaceDate,CategoryId,Anonymous,Archived")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //string fileName = UploadFile(postImageFile);
                    //post.Image = fileName;

                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", post.CategoryId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }

        [HttpPost]
        public async Task ReportPost([FromBody] ReportedPost reportedPost)
        {
            AppUser reporter = await _userManager.GetUserAsync(User);
            reportedPost.UserId = reporter.Id;
            reportedPost.Post = _context.Posts.Single(p => p.PostId == reportedPost.PostId);
            await _context.ReportedPosts.AddAsync(reportedPost);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task ReportComment([FromBody] ReportedComment reportedComment)
        {
            AppUser reporter = await _userManager.GetUserAsync(User);
            reportedComment.UserId = reporter.Id;
            reportedComment.Comment = await _context.Comments.SingleAsync(p => p.CommentId == reportedComment.CommentId);
            await _context.ReportedComments.AddAsync(reportedComment);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task ReporSolution([FromBody] ReportedSolution reportedSolution)
        {
            AppUser reporter = await _userManager.GetUserAsync(User);
            reportedSolution.UserId = reporter.Id;
            reportedSolution.Solution = await _context.Solutions.SingleAsync(p => p.SolutionId == reportedSolution.SolutionId);
            await _context.ReportedSolution.AddAsync(reportedSolution);
            await _context.SaveChangesAsync();
        }
    }
}
