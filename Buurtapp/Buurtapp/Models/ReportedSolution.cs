using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("ReportedSolution")]
    public class ReportedSolution : Report
    {
        public ReportedSolution() : base()
        {
        }

        [Column("Solution")]
        public int SolutionId { get; set; }

        public Solution Solution { get; set; }
    }
}
