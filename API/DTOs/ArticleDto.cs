using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ArticleDto
    {
        [Required] public string Title { get; set; }
        public string UrlIdentity { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        [Required] public string Contents { get; set; }
        [Required] public string Tags { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}