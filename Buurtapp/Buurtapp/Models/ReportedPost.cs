﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("ReportedPost")]
    public class ReportedPost : Report
    {
        public ReportedPost() : base()
        {
        }

        [Column("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
