using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Photo
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}
