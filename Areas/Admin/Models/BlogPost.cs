using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class BlogPost
    {
        [Key]
        public int BlogPostId { get; set; }
        [Required]
        public string BlogPostTitle { get; set; }
        [Required]
        public string BlogPostDescription { get; set; }
        [Required]
        public string BlogPostAuthor { get; set; }
        [Required]
        public DateTime BlogPostDate { get; set; } = DateTime.Now;
        public ICollection<Comment>? Comments { get; set; }


    }
}

