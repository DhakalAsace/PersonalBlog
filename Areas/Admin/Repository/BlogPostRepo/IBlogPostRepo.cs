using Blog.Models;

namespace PersonalBlog.Areas.Admin.Repository.BlogPostRepo
{
    public interface IBlogPostRepo
    {
        public ICollection<BlogPost> GetAllBlogs();
        public ICollection<BlogPost> GetBlogByAuthor(string authorName);
        public ICollection<BlogPost> GetBlogsByDate(DateTime dateTime);
        public BlogPost GetBlogById(int id);

        public void Edit(BlogPost blog);
        public void Create(BlogPost blog);
        public void Delete(BlogPost blog);


    }
}
