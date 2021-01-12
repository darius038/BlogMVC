using BlogMVC.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Web.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int? CategoryId { get; set; }
        public string CurrentImage { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; }
        public IFormFile Image { get; set; } = null;
        public IEnumerable<Category> Categories { get; set; } = null;
    }
}
