namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }
    }
}