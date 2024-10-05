using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace NewsPaper.Models.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of department :(")]
        [StringLength(50, ErrorMessage = "The name must not exceed 50 characters :(")]
        [MinLength(3, ErrorMessage = "Invalid Name :(")]
        public string? Name { get; set; }

        [Display(Name = "BirthDate")]
        [DataType(DataType.Date)]
        [DateInRange(2000, 2024, ErrorMessage = "Birthdate must be between the years 2000 and 2024.")]
        public DateTime? BirthDate { get; set; }
        public virtual ICollection<Author>? authors{ get; set; }
    }
}
