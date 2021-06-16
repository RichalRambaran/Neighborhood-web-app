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
    public class ChatParticipantController : Controller
    {
        private readonly UserContext _context;

        public ChatParticipantController(UserContext context)
        {
            _context = context;
        }

        // GET: ChatParticipant
        public async Task<IActionResult> Index()
        {
            var userContext = _context.ChatParticipants.Include(c => c.AppUser).Include(c => c.Chat);
            return View(await userContext.ToListAsync());
        }

        // GET: ChatParticipant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatParticipant = await _context.ChatParticipants
                .Include(c => c.AppUser)
                .Include(c => c.Chat)
                .FirstOrDefaultAsync(m => m.ChatId == id);
            if (chatParticipant == null)
            {
                return NotFound();
            }

            return View(chatParticipant);
        }

        // GET: ChatParticipant/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId");
            return View();
        }

        // POST: ChatParticipant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChatId,UserId")] ChatParticipant chatParticipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatParticipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", chatParticipant.UserId);
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", chatParticipant.ChatId);
            return View(chatParticipant);
        }

        // GET: ChatParticipant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatParticipant = await _context.ChatParticipants.FindAsync(id);
            if (chatParticipant == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", chatParticipant.UserId);
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", chatParticipant.ChatId);
            return View(chatParticipant);
        }

        // POST: ChatParticipant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChatId,UserId")] ChatParticipant chatParticipant)
        {
            if (id != chatParticipant.ChatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatParticipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatParticipantExists(chatParticipant.ChatId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", chatParticipant.UserId);
            ViewData["ChatId"] = new SelectList(_context.Chats, "ChatId", "ChatId", chatParticipant.ChatId);
            return View(chatParticipant);
        }

        // GET: ChatParticipant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatParticipant = await _context.ChatParticipants
                .Include(c => c.AppUser)
                .Include(c => c.Chat)
                .FirstOrDefaultAsync(m => m.ChatId == id);
            if (chatParticipant == null)
            {
                return NotFound();
            }

            return View(chatParticipant);
        }

        // POST: ChatParticipant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatParticipant = await _context.ChatParticipants.FindAsync(id);
            _context.ChatParticipants.Remove(chatParticipant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatParticipantExists(int id)
        {
            return _context.ChatParticipants.Any(e => e.ChatId == id);
        }
    }
}
