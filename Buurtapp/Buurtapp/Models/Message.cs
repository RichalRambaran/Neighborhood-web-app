using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Message")]
    public class Message
    {
        public Message()
        {
        }

        [Key]
        [Column("MessageId")]
        public int MessageId { get; set; }

        [Column("Sender")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Chat")]
        public int ChatId { get; set; }

        public Chat Chat { get; set; }

        [Column("Content")]
        public string Content { get; set; }

        [Column("SendDate")]
        public string SendDate
        {
            get { return DateTime.Today.ToString("dd-MM-yyyy"); }
            set { }
        }

        [Column("SendTime")]
        public string SendTime
        {
            get { return DateTime.Now.ToString("HH:mm"); }
            set { }
        }
    }
}
