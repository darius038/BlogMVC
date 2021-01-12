using System;

namespace BlogMVC.Domains
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public string CommentUserName { get; set; }

    }
}
