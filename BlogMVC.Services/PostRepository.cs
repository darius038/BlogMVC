using BlogMVC.Data;
using BlogMVC.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        // ADD post
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        // GET all posts
        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        // GET post by id
        public Post GetPost(int id)
        {
            return _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);            
        }

        // DELETE post
        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        // UPDATE post
        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        // GET posts by category
        public List<Post> GetByCategory(int id)
        {
            return _context.Posts.Where(i => i.CategoryId == id).ToList();
        }

        // SAVE changes
        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
