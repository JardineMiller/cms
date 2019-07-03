using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms.Data_Layer.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Author")]
        public string AuthorId;

        [Required]
        public string Title;

        public string Body;

        public string Snippet;

        public User Author;

        public DateTimeOffset Timestamp;

        public List<Comment> Comments { get; set; }

        [Required]
        public string Content { get; set; }
    }
}