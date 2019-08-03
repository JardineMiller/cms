using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms.Data_Layer.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Author")]
        public long AuthorId { get; set; }

        public User Author { get; set; }

        [Required] public DateTimeOffset Timestamp;

        [Required]
        public string Content { get; set; }

        public List<Comment> Replies { get; set; }
    }
}