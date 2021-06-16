using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("ChatParticipant")]
    public class ChatParticipant
    {
        public ChatParticipant()
        {
        }

        [Column("Chat")]
        public int ChatId { get; set; }

        public Chat Chat { get; set; }

        [Column("Participant")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
