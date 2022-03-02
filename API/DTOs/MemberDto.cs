namespace API.DTOs
{
    public class MemberDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }
    }
}