using BlogMVC.Domains;
using BlogMVC.Web.ViewModels;
using System.Collections.Generic;

namespace BlogMVC.Web.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool NextPage { get; set; }
        public string Category { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<int> Pages { get; internal set; }
    }
}
