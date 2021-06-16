using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Comment")]
    public class Comment
    {
        public Comment()
        {
        }

        [Key]
        [Column("CommentId")]
        public int CommentId { get; set; }

        [Column("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Column("Commenter")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Content")]
        public string Content { get; set; }

        [Column("PlaceDate")]
        public string PlaceDate { get; set; }

        public List<ReportedComment> ReportedComments { get; set; } = new List<ReportedComment>();
    }
}
