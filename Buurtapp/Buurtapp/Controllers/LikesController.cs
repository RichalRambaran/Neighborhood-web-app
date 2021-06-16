using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buurtapp.Data;
using Buurtapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Buurtapp.Controllers
{
    public class LikesController : Controller
    {
        private readonly UserContext _context;

        public LikesController(UserContext context)
        {
            _context = context;

        }


        public IActionResult WijzigLike(int id)
        {
            var user = _context.Users.Where(p => p.Email == User.Identity.Name).First();
            var userLikesPost = _context.UserLikesPosts.Where(p => p.PostId == id && p.UserId == user.Id);
            var toegevoegd = 1;


            if (userLikesPost.Count() > 0) {
                _context.Entry(userLikesPost.First()).State = EntityState.Deleted;
                toegevoegd = 0;

            } 
            else {
                var x = new UserLikesPost { UserId = user.Id, PostId = id };
                _context.Entry(x).State = EntityState.Added;
            }
            _context.SaveChanges();
            return Json(new { postid = id, toegevoegd = toegevoegd });
        }
    }
}
