namespace API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}