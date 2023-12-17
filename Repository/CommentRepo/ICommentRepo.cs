using Blog.Models;
using PersonalBlog.ViewModels;

namespace PersonalBlog.Repository.CommentRepo
{
    public interface ICommentRepo
    {
        public void CreateComment(Comment comment);
        public void EditComment(int id);
        public void DeleteComment(int id);
        public ICollection<Comment> GetCommentByBlogPosts(int BlogId);
    }
}
