using System.ComponentModel.DataAnnotations;

namespace Blog.Client.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string Description { get; set; }
        public string DataDodania { get; set; }

        public string PostId { get; set; }

    }
}
