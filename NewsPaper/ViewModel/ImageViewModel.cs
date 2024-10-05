using NewsPaper.Models.Entities;

namespace NewsPaper.ViewModel
{
    public class ImageViewModel
    {
        public int Id { get; set; } 
        public IFormFile? Image { get; set; } //
        //public string imagePath { get; set; }
        public string? PostType { get; set; } //
        public DateTime? CreateTime { get; set; } //
        public string? Description { get; set; } //
        public int? AuthorId { get; set; } //
        public virtual Author? author { get; set; }

    }
}
