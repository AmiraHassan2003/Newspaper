using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsPaper.Models.Entities
{
    public enum gender{
        Female, Male
    }
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name :(")]
        [StringLength(50, ErrorMessage = "The name must not exceed 50 characters :(")]
        [MinLength(3, ErrorMessage = "Invalid Name :(")]
        //[Display(Name = "Author Name")]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter your title job :(")]
        public string? TitleJob { get; set; }

        //[Required(ErrorMessage = "Please select a gender :(")]
        public gender? Gender { get; set; }

        public double? Salary { get; set; }

        public string? Address { get; set; }

        [ForeignKey(nameof(Department))]
        public int? departmentId { get; set; }
        public virtual Department? department { get; set; }

        public virtual ICollection<Post>? posts {get; set; }
    }
}
