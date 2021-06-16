using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("ReportedComment")]
    public class ReportedComment : Report
    {
        public ReportedComment() : base()
        {
        }

        [Column("Comment")]
        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
