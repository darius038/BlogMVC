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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);       
        }

        public List<Category> GetAllCategories()
        {
            var cat = _context.Categories.ToList<Category>();

            foreach (var item in cat)
            {
                item.Posts = _context.Posts.Where(i => i.CategoryId == item.Id).ToList();
            }

            return _context.Categories.ToList<Category>();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(i => i.Id == id);
        }

        public void RemoveCategory(int id)
        {
            _context.Categories.Remove(_context.Categories.FirstOrDefault(i => i.Id == id));            
        }

        public void UpdateCategory(int id, Category category)
        {
            var temp = GetCategory(id);
            temp.Name = category.Name;
            _context.Update(temp);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public bool hasPost(int id)
        {
           var item = GetCategory(id);

            if (item.Posts == null)
            {
                return false;
            }
            return true;


        }
    }
}
