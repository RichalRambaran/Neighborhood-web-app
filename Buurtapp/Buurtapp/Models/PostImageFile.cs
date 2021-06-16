using System;
using Microsoft.AspNetCore.Http;

namespace Buurtapp.Models
{
    public class PostImageFile
    {
        public PostImageFile()
        {
        }

        public IFormFile Image { get; set; }
    }
}
