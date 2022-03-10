namespace API.DTOs
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public string Headline { get; set; }
        public string Contents { get; set; }
        public string Tags { get; set; }
    }
}