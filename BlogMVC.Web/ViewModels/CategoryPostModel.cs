using BlogMVC.Domains;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace BlogMVC.Web.ViewModels
{
    public class CategoryPostModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public Post Post { get; set; }
        public Category Category { get; set; }
        public string CurrentImage { get; set; }
        public IFormFile Image { get; set; } = null;

    }
}
