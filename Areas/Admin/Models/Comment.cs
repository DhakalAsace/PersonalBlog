using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        [Required]
        public int CommentId { get; set; }

        // Foreign key for the associated blog post
        public int BlogPostId { get; set; }
        [Required]
        public string CommentDesc { get; set; }
        public string UserName { get; set; }
        [Required]
        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}
