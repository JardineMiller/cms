using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace cms.Data_Layer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}