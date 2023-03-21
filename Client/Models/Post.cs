using System.ComponentModel.DataAnnotations;

namespace Blog.Client.Models
{
    public class Post
    {
        public string PostId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimum required length of 5 characters")]
        public string Title { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Minimum required length of 20 characters")]
        public string Description { get; set; }
        [Required]
        [MinLength(20, ErrorMessage = "Required picture")]
        public string Photo { get; set; }
        public string DataDodania { get; set; }
        [Required(ErrorMessage = "Required category")]
        public Category PostCategory { get; set; }
    }
}
