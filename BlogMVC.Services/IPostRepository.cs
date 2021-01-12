using BlogMVC.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Repository
{
    public interface IPostRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();       
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);       
        Task<bool> SaveChangesAsync();
        List<Post> GetByCategory(int id);
    }
}
