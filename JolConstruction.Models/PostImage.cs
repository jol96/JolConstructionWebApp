using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JolConstruction.Models
{
    public class PostImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
