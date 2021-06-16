using Buurtapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buurtapp.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public Post Post { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Solution> Solutions { get; set; }
    }
}
