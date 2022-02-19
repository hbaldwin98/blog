namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }
    }
}