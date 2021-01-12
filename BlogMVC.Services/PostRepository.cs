using BlogMVC.Data;
using BlogMVC.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Repository
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        
        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);            
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public List<Post> GetByCategory(int id)
        {
            return _context.Posts.Where(i => i.CategoryId == id).ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }



        //public void AddSubComment(SubComment comment)
        //{
        //    _context.SubComments.Add(comment);
        //}
    }
}
