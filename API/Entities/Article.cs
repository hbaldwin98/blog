using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Articles")]
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateEdited { get; set; }
        public string Contents { get; set; }
        public string Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    } 
}