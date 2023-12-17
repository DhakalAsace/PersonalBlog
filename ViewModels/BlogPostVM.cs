using Blog.Models;

namespace PersonalBlog.ViewModels
{
    public class BlogPostVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime BlogPostDate { get; set; } = DateTime.Now;
        public List<CommentVM> Comments { get; set; }
    }


}
