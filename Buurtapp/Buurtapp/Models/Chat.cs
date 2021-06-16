using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Chat")]
    public class Chat
    {
        public Chat()
        {
        }

        [Key]
        [Column("ChatId")]
        public int ChatId { get; set; }

        [Column("Owner")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("CreateDate")]
        public string CreateDate
        {
            get { return DateTime.Today.ToString("dd-MM-yyyy"); }
            set { }
        }

        public ICollection<ChatParticipant> ChatParticipants { get; set; }
    }
}
