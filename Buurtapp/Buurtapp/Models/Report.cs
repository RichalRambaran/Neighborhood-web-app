using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Report")]
    public abstract class Report
    {
        public Report()
        {
        }

        [Key]
        [Column("ReportId")]
        public int ReportId { get; set; }

        [Column("Reporter")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
