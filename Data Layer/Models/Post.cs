using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms.Data_Layer.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Author")]
        public long AuthorId { get; set; }

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
        public DateTimeOffset Timestamp { get; set; }

        public List<Comment> Comments { get; set; }
    }
}