using System;
using System.Collections.Generic;

namespace BlogMVC.Domains
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public ICollection<Post> Posts { get; set; }

    }
}
