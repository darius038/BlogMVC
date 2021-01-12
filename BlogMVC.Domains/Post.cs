using System;
using System.Collections.Generic;

namespace BlogMVC.Domains
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";       
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public List<Comment> Comments { get; set; }
    }
}
