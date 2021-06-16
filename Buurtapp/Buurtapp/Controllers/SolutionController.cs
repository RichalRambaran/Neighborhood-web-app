using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Buurtapp.Data;
using Buurtapp.Models;

namespace Buurtapp.Controllers
{
    public class SolutionController : Controller
    {
        private readonly UserContext _context;

        public SolutionController(UserContext context)
        {
            _context = context;
        }

        // GET: Solution
        public async Task<IActionResult> Index()
        {
            var userContext = _context.Solutions.Include(s => s.AppUser).Include(s => s.Post);
            return View(await userContext.ToListAsync());
        }

        // GET: Solution/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions
                .Include(s => s.AppUser)
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.SolutionId == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Solution/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId");
            return View();
        }

        // POST: Solution/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolutionId,PostId,UserId,Title,Description,Votes,PlaceDate")] Solution solution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", solution.UserId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", solution.PostId);
            return View(solution);
        }

        // GET: Solution/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", solution.UserId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", solution.PostId);
            return View(solution);
        }

        // POST: Solution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolutionId,PostId,UserId,Title,Description,Votes,PlaceDate")] Solution solution)
        {
            if (id != solution.SolutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionExists(solution.SolutionId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", solution.UserId);
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "PostId", solution.PostId);
            return View(solution);
        }

        // GET: Solution/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions
                .Include(s => s.AppUser)
                .Include(s => s.Post)
                .FirstOrDefaultAsync(m => m.SolutionId == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // POST: Solution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "Post");
        }

        private bool SolutionExists(int id)
        {
            return _context.Solutions.Any(e => e.SolutionId == id);
        }
    }
}
