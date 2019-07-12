using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms.Data_Layer.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Author")]
        public string AuthorId;

        public User Author;

        public DateTimeOffset Timestamp;

        [Required]
        public string Content { get; set; }
    }
}