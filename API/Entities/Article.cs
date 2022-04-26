using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Articles")]
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UrlIdentity { get; set; }
        public AppUser Author { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateEdited { get; set; } = DateTime.UtcNow;
        public string Headline { get; set; }
        public string Contents { get; set; }
        public string Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    } 
}