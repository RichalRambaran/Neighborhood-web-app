using Buurtapp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.Models
{
    [Table("Location")]
    public class Location
    {
        public Location()
        {
        }

        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [Column("HouseNumber")]
        public int HouseNumber { get; set; }

        [Column("Neighbourhood")]
        public string Neighbourhood { get; set; }

        public List<AppUser> AppUsers { get; set; } = new List<AppUser>();
    }
}
