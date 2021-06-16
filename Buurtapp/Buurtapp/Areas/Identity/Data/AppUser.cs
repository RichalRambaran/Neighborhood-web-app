using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Buurtapp.Models;
using Microsoft.AspNetCore.Identity;

namespace Buurtapp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AppUser class
    [Table("User")]
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
        }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [Column("HouseNumber")]
        public int HouseNumber { get; set; }

        public Location Location { get; set; }

        [Column("CreateDate")]
        public string CreateDate
        {
            get { return DateTime.Today.ToString("dd-MM-yyyy"); }
            set { }
        }

        public List<Post> Posts { get; set; } = new List<Post>();

        public ICollection<UserLikesPost> UserLikesPosts { get; set; }

        public List<Solution> Solutions { get; set; } = new List<Solution>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<Chat> Chats { get; set; } = new List<Chat>();

        public ICollection<ChatParticipant> ChatParticipants { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();

        public List<Report> Reports { get; set; } = new List<Report>();

        [NotMapped]
        public string Fullname { get { return FirstName + " " + LastName; } }
    }
}
