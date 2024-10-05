using Azure.Core.Pipeline;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPaper.Models.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Type Of Post")]
        [Display(Name = "Post Type")]
        public string? PostType { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreateTime { get; set; }

        [MinLength(3, ErrorMessage = "please enter the descripton")]
        public string? Description { get; set; }
        public string? Image { get; set; }

        [ForeignKey(nameof(Author))]
        public int? AuthorId { get; set; }
        
        public virtual Author? author { get; set; }
    }
}
