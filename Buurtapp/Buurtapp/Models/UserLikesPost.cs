using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("UserLikesPost")]
    public class UserLikesPost
    {
        public UserLikesPost()
        {
        }

        [Column("User")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
