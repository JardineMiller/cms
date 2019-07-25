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
                int snippetLength;
                bool includeElipsis = true;
                if (Body.Length < 196)
                {
                    snippetLength = Body.Length;
                    includeElipsis = false;
                }
                else
                {
                    snippetLength = 196;
                }
                
                var snippet = Body.Substring(0, snippetLength);

                if (includeElipsis) snippet += "...";

                return snippet;
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