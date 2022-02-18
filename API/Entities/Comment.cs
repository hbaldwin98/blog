namespace API.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommenterName { get; set; }
        public DateTime DateCommented { get; set; } = DateTime.Now;
        public string Contents { get; set; }
        public int ArticleId { get; set; }
    }
}