using Blog.Models;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using System.Linq;

namespace PersonalBlog.Areas.Admin.Repository.BlogPostRepo
{
    public class BlogPostRepoImpl : IBlogPostRepo
    {
        private readonly AppDbContext _db;
        public BlogPostRepoImpl (AppDbContext db)
        {
            _db = db;
        }

        public void Create(BlogPost blog)
        {
            _db.BlogPosts.Add(blog);
            _db.SaveChanges();
        }
        public void Edit(BlogPost blog)
        {   var existingBlogPost= _db.BlogPosts.Find(blog.BlogPostId);
            if (existingBlogPost != null)
            {
                existingBlogPost.BlogPostDescription = blog.BlogPostDescription;
                existingBlogPost.BlogPostAuthor = blog.BlogPostAuthor;
                existingBlogPost.BlogPostTitle = blog.BlogPostTitle;
                existingBlogPost.BlogPostId = existingBlogPost.BlogPostId;
                existingBlogPost.BlogPostDate = blog.BlogPostDate;
                _db.Entry(existingBlogPost).State = EntityState.Modified;
                _db.SaveChanges();

            }

        }
        public ICollection<BlogPost> GetBlogByAuthor(string authorName)
        {
            var authorBlogs = _db.BlogPosts.Where<BlogPost>(c=>c.BlogPostAuthor==authorName).ToList();
            return authorBlogs;
        }

        public BlogPost GetBlogById(int id)
        {
            var blogs = _db.BlogPosts.FirstOrDefault(c => c.BlogPostId == id);
            return blogs;
        }

        void IBlogPostRepo.Delete(BlogPost blog)
        {
                _db.BlogPosts.Remove(blog);
                _db.SaveChanges();
            
        }

        ICollection<BlogPost> IBlogPostRepo.GetAllBlogs()
        {
            return _db.BlogPosts.ToList();
        }

        ICollection<BlogPost> IBlogPostRepo.GetBlogsByDate(DateTime dateTime)
        {
            var dateBlogs = _db.BlogPosts.Where<BlogPost>(c => c.BlogPostDate.Equals(dateTime)).ToList();
            return dateBlogs;
        }
    }
}
