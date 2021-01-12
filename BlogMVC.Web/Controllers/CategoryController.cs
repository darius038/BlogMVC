using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogMVC.Data;
using BlogMVC.Domains;
using Microsoft.AspNetCore.Authorization;
using BlogMVC.Repository;
using AutoMapper;
using BlogMVC.Web.ViewModels;

namespace BlogMVC.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _repository;
        private IPostRepository _postrepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IPostRepository postrepository, IMapper mapper)
        {
            _repository = repository;
            _postrepository = postrepository;
            _mapper = mapper;
        }

        // GET: Category/
        public IActionResult Index()
        {
            return View(_repository.GetAllCategories());
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var category = _mapper.Map<Category>(model);
            _repository.AddCategory(category);

            if (await _repository.SaveChangesAsync())
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Category/Edit/id
        public IActionResult Edit(int id)
        {
            var category = _repository.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/id        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(model);
                    _repository.UpdateCategory(id, category);
                    await _repository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Category/Delete/id
        public IActionResult Delete(int id)
        {

            var category = _repository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            
            var categoryModel = new CategoryViewModel()
            {
                Name = category.Name,
                Id = category.Id           
            };

            var posts = _postrepository.GetByCategory(id);
            if (posts.Count>0)
            {
                categoryModel.havePost = true;
            }

            return View(categoryModel);
        }

        // POST: Category/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _repository.RemoveCategory(id);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
