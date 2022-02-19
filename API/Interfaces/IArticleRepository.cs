using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IArticleRepository
    {
        public Task<ArticleDto> GetArticle(int id);
        public void DeleteArticle();
        public Task<bool> SaveAllAsync();
    }
}