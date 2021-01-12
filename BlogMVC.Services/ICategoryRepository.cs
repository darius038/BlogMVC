using BlogMVC.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogMVC.Repository
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        Category GetCategory(int id);
        List<Category> GetAllCategories();
        void RemoveCategory(int id);
        void UpdateCategory(int id, Category category);
        Task<bool> SaveChangesAsync();
        bool hasPost(int id);

    }
}
