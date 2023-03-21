using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostsComments.Api.Models
{
    public class Post
    {
        [Key]
        [Required]
        public string PostId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Photo { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string DataDodania { get; set; }
        [Required]
        public Category PostCategory { get; set; }


#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public List<Comment>? Comments { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.


    }
}
