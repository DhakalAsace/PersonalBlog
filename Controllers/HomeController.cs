using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonalBlog.Areas.Admin.Repository.BlogPostRepo;
using PersonalBlog.Models;
using PersonalBlog.Repository.CommentRepo;
using PersonalBlog.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace PersonalBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly ICommentRepo _commentRepo;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepo blogPostRepo, ICommentRepo commentRepo)
        {
            _logger = logger;
            _blogPostRepo = blogPostRepo;
            _commentRepo = commentRepo;
        }

        public IActionResult Index()
        {
            var blogs = _blogPostRepo.GetAllBlogs();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult BlogDetails(int id)
        {
            var blogPost = _blogPostRepo.GetBlogById(id);
            var blogComments = _commentRepo.GetCommentByBlogPosts(id);
            var viewModel = new BlogPageVM
            {
                Post = new BlogPostVM
                {
                    BlogPostDate = blogPost.BlogPostDate,
                    Author = blogPost.BlogPostAuthor,
                    Title = blogPost.BlogPostTitle,
                    Description = blogPost.BlogPostDescription,
                    Comments = blogComments.Select(p => new CommentVM { CommentDate = p.CommentDate, CommentDesc = p.CommentDesc, UserName = p.UserName }).ToList(),
                },
                Comment = new CommentVM
                {
                    BlogPostId = id,
                }
            };
            return View(viewModel);
        }

       [HttpPost]
public IActionResult BlogDetails(int id, CommentVM comment)
{
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var newComment = new Comment
                    {
                        BlogPostId = id,
                        CommentDate = DateTime.Now,
                        CommentDesc = comment.CommentDesc,
                        UserName = User.Identity.Name
                    };

                    _commentRepo.CreateComment(newComment);

                    // Redirect back to the blog details page after submitting the comment.
                    return RedirectToAction("BlogDetails", new { id });
                }
               
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            }
            // If the comment submission is not valid, return to the same view with validation errors.
            var blogPost = _blogPostRepo.GetBlogById(id);
    var blogComments = _commentRepo.GetCommentByBlogPosts(id);
    var viewModel = new BlogPageVM
    {
        Post = new BlogPostVM
        {
            BlogPostDate = blogPost.BlogPostDate,
            Author = blogPost.BlogPostAuthor,
            Title = blogPost.BlogPostTitle,
            Description = blogPost.BlogPostDescription,
            Comments = blogComments.Select(p => new CommentVM { CommentDate = p.CommentDate, CommentDesc = p.CommentDesc, UserName = p.UserName, CommentId = p.CommentId }).ToList(),
        },
        Comment = comment  // Pass the comment back to the view to show validation errors.
    };

    return View(viewModel);
}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
