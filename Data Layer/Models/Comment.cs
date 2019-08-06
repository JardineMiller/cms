using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace cms.Data_Layer.Models
{
    public class Comment
    {
        
        // Properties
        
        [Key]
        public long Id { get; set; }

        public DateTimeOffset Timestamp;

        [Required]
        public string Content { get; set; }

        public List<Comment> Replies { get; set; }
        
        
        
        // Relationships
        
        public long AuthorId { get; set; }

        public User Author { get; set; }

        public long? PostId { get; set; }
        
        [JsonIgnore]
        public Post Post { get; set; }
        
        public long? ParentCommentId { get; set; }

        [JsonIgnore]
        public Comment ParentComment { get; set; }

    }
}