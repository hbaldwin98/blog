using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string UrlIdentity { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }
        public string Contents { get; set; }
        public string Tags { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}