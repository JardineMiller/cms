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
        public int AuthorId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Body { get; set; }

        [NotMapped]
        public string Snippet
        {

            get
            {
                var bodyLength = Body.Length;
                var snippetLength = bodyLength < 196 ? bodyLength : 196;
                return Body.Substring(0, snippetLength) + "...";
            }
            private set { }
        }

        public User Author { get; private set; }

        [Required]
        public DateTimeOffset Timestamp { get; private set; }

        public List<Comment> Comments { get; set; }

        public Post(int authorId, string title, string body)
        {
            AuthorId = authorId;
            Title = title;
            Body = body;
            Timestamp = DateTimeOffset.UtcNow;
            Comments = new List<Comment>();
        }
    }
}