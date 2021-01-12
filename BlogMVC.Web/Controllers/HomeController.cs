using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogMVC.Domains;
using BlogMVC.Repository;
using BlogMVC.Web.ViewModels;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BlogMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPostRepository _repository;
        private ICategoryRepository _catrepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository repository, ICategoryRepository catrepository)
        {
            _logger = logger;
            _repository = repository;
            _catrepository = catrepository;
            
        }

        // GET: Post/all
        public IActionResult Index()
        {
            
            CategoryPostModel categoryPostModel = new CategoryPostModel 
            { 
                Posts = _repository.GetAllPosts(), 
                Categories = _catrepository.GetAllCategories() 
            };
            categoryPostModel.Posts = categoryPostModel.Posts.Reverse();

            return View(categoryPostModel);
        }

        // GET: Post/id
        public IActionResult Post(int id)
        {
            var post = _repository.GetPost(id);
            var category = _catrepository.GetCategory((int)post.CategoryId);
            ViewBag.Category = category.Name;
            return View(post);
        }

        // GET: Post/Category/id
        public IActionResult Category(int id)
        {
            CategoryPostModel categoryPostModel = new CategoryPostModel 
            { 
                Posts = _repository.GetByCategory(id), 
                Categories = _catrepository.GetAllCategories()
            };
            
            ViewBag.CategoryId = id;

            categoryPostModel.Posts = categoryPostModel.Posts.Reverse();

            return View(categoryPostModel);
        }

        //POST: Comment
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = model.PostId });

            var post = _repository.GetPost(model.PostId);

            //Add to Post new List<Comments> if it is null
            post.Comments = post.Comments ?? new List<Comment>();

            post.Comments.Add(new Comment
            {
                Message = model.Message,
                Created = DateTime.Now,
                CommentUserName = model.CommentUserName
            });

            _repository.UpdatePost(post);
            await _repository.SaveChangesAsync();

            return RedirectToAction("Post", new { id = model.PostId });
        }

        public IActionResult Example(int id)
        {
            var post = _repository.GetPost(id);
            var category = _catrepository.GetCategory((int)post.CategoryId);
            ViewBag.Category = category.Name;
            return View(post);
        }


        //Default error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            CategoryPostModel categoryPostModel = new CategoryPostModel
            {
                Posts = _repository.GetAllPosts(),
                Categories = _catrepository.GetAllCategories()
            };
            categoryPostModel.Posts = categoryPostModel.Posts.Reverse();

            return View(categoryPostModel);
        }
    }
}
