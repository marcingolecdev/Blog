using System.ComponentModel.DataAnnotations;

namespace News.Api.Models
{
    public class Message
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string DataDodania { get; set; }
    }
}
