using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp1.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }

}
