using Buurtapp.Areas.Identity.Data;
using Buurtapp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;


namespace Buurtapp.Models
{
    [Table("Post")]
    public class Post
    {


        public Post()
        {
        }

        [Key]
        [Column("PostId")]
        public int PostId { get; set; }

        [Column("Poster")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Title")]
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be less than 3.", MinimumLength = 3)]
        public string Title { get; set; }

        [Column("Content")]
        [Required]
        [StringLength(255, ErrorMessage = "Name length can't be less than 10.", MinimumLength = 10)]
        public string Content { get; set; }

        [Column("Views")]
        public int Views { get; set; }

        [Column("Image")]
        public string Image { get; set; }

        [Column("PlaceDate")]
        public string PlaceDate { get; set; }

        [Column("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Column("Anonymous")]
        public bool Anonymous { get; set; }

        [Column("Archived")]
        public bool Archived { get; set; }

        public List<Solution> Solutions { get; set; } = new List<Solution>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<UserLikesPost> UserLikesPosts { get; set; }

        public List<ReportedPost> ReportedPosts { get; set; } = new List<ReportedPost>();

        [NotMapped]
        public int AantalLikes { get { return UserLikesPosts == null ? 0 : UserLikesPosts.Count(); } }


        //public int GetAmountOfLikes
        //{
        //    get { return _context.UserLikesPosts.Where(p => p.PostId == this.PostId).Count(); }
        //}

        [NotMapped]
        public string likedByMe { get; set; }

    }
}
