using BlogMVC.Data;
using BlogMVC.Domains;
using System.Collections.Generic;
using System.Linq;
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

        // ADD new category
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);       
        }

        // GET all categories
        public List<Category> GetAllCategories()
        {
            var cat = _context.Categories.ToList<Category>();

            foreach (var item in cat)
            {
                item.Posts = _context.Posts.Where(i => i.CategoryId == item.Id).ToList();
            }

            return _context.Categories.ToList<Category>();
        }

        // GET categorie by id
        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(i => i.Id == id);
        }

        // GET remove categorie
        public void RemoveCategory(int id)
        {
            _context.Categories.Remove(_context.Categories.FirstOrDefault(i => i.Id == id));            
        }

        // UPDATE categorie
        public void UpdateCategory(int id, Category category)
        {
            var temp = GetCategory(id);
            temp.Name = category.Name;
            _context.Update(temp);
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
