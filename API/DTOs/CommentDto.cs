namespace API.DTOs
{
    public class CommentDto
    {
        public string CommenterName { get; set; }
        public string Contents { get; set; }
        public DateTime DateCommented { get; set; }
    }
}