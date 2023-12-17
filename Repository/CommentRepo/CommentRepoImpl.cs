using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Repository.CommentRepo;
using PersonalBlog.ViewModels;
using System.Security.Claims;

namespace PersonalBlog.Areas.Admin.Repository.CommentRepo
{
    public class CommentRepoImpl : ICommentRepo
    {
        private readonly AppDbContext _db;
        public CommentRepoImpl(AppDbContext appDbContext) 
        {
            _db=appDbContext;
        }
        public void CreateComment(Comment comment)
        {
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }


        public void DeleteComment(int id)
        {
            var comment = _db.Comments.FirstOrDefault(c=>c.CommentId == id);
            if (comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }
        }

        public void EditComment(int id)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.CommentId == id);
            if (comment != null)
            {
                _db.Comments.Update(comment);
                _db.SaveChanges();
            }
        }

        public ICollection<Comment> GetCommentByBlogPosts(int BlogId)
        {
            var comments= _db.Comments.Where(c=>c.BlogPostId==BlogId).ToList();
            return comments;
        }
    }
}
