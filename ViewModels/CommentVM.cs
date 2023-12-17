using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.ViewModels
{
    public class CommentVM
    {
        public int BlogPostId { get; set; }
        public string CommentDesc { get; set; }
        public string UserName { get; set; }
        public DateTime CommentDate { get; set; }
        public int CommentId { get; set; }



    }
}
