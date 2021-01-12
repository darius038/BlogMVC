using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using BlogMVC.Web.ViewModels;
using BlogMVC.Domains;
using BlogMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogMVC.Repository.FileManager;

namespace BlogMVC.Web.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private IPostRepository _repository;
        private readonly IMapper _mapper;
        private IFileManager _fileManager;
        private ICategoryRepository _catrepository;

        public AdminPanelController(IPostRepository repository, IMapper mapper, 
            IFileManager fileManager, ICategoryRepository catrepository)
        {
           _mapper = mapper;
           _repository = repository;
           _fileManager = fileManager;
           _catrepository = catrepository;
        }

        //GET: Posts
        public IActionResult Index()
        {
            var postList = _repository.GetAllPosts();
            return View(postList);           
        }

        //GET: Edit/Create Post
        [HttpGet]
        public IActionResult Edit(int? id)
        {      
            var categories = _catrepository.GetAllCategories();
            if (id == null)
            {
                return View(new CategoryPostModel() { Post = new Post(), Categories= categories });
            }

            var post = _repository.GetPost((int)id);

            return View(new CategoryPostModel
            {
                Post = post,
                CurrentImage = post.Image,
                Categories = categories
            });
        }

        //POST: Edit/Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = _mapper.Map<Post>(model.Post);

            // logika perkelti i repo metodus
            if (model.Image == null)
            {
                post.Image = model.CurrentImage;
            }
            else
            {             
                post.Image = await _fileManager.SaveImage(model.Image);
            }                

            // logika perkelti i repo metodus
            if (post.Id > 0)
            {
                _repository.UpdatePost(post);
            }
            else
            {
                _repository.AddPost(post);
            }
            
            
            if (await _repository.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            return View(model);

        }

        //GET: Delete Post
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repository.RemovePost((int)id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
