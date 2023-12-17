using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Areas.Admin.Repository.BlogPostRepo;
using PersonalBlog.Data;

namespace PersonalBlog.Areas.Admin.Controllers

{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class HomeController : Controller
    {
        private IBlogPostRepo _blogPostRepo;
        public HomeController( IBlogPostRepo blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
        }
        public IActionResult Index()
        {
           var allBlogs= _blogPostRepo.GetAllBlogs();
            return View(allBlogs);
        }
        [HttpGet]
        public IActionResult BlogCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult BlogCreate(BlogPost blogPost)
        {
            if(ModelState.IsValid)
            {
                _blogPostRepo.Create(blogPost);
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        [HttpGet]
        public IActionResult BlogEdit(int id)
        {
            var blogPost = _blogPostRepo.GetBlogById(id);
            if (blogPost!= null)
            {
                return View(blogPost);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult BlogEdit(BlogPost blog)
        {
            if (ModelState.IsValid)
            {
                _blogPostRepo.Edit(blog);
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        [HttpGet]
        public IActionResult BlogDelete(int id) 
        {
            var blogPost=_blogPostRepo.GetBlogById(id);
            return View(blogPost);
        }
        [HttpPost]
        public IActionResult BlogDelete(BlogPost blog)
        {
            _blogPostRepo.Delete(blog);
            return RedirectToAction("Index");
        }
       

    }
}
