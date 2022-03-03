using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CommentDto
    {
        public string CommenterName { get; set; }
        public int Id { get; set; }
        public string Contents { get; set; }
        public DateTime DateCommented { get; set; }
    }
}