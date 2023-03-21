using System.ComponentModel.DataAnnotations;

namespace Blog.Client.Models
{
    public class Message
    {
        public string Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimum required length of 5 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum required length of 10 characters")]
        public string Description { get; set; }
        public string DataDodania { get; set; }
    }
}
