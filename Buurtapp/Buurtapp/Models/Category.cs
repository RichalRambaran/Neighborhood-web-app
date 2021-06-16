using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buurtapp.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
        }

        [Key]
        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
