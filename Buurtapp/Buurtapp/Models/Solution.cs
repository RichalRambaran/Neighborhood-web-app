using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Solution")]
    public class Solution
    {
        public Solution()
        {
        }

        [Key]
        [Column("SolutionId")]
        public int SolutionId { get; set; }
        
        [Column("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; } = new Post();

        [Column("Proposer")]
        public string UserId { get; set; }

        public AppUser AppUser { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Votes")]
        public int Votes { get; set; }

        [Column("PlaceDate")]
        public string PlaceDate
        {
            get { return DateTime.Today.ToString("dd-MM-yyyy"); }
            set { }
        }

        public List<ReportedSolution> ReportedSolutions { get; set; } = new List<ReportedSolution>();
    }
}
