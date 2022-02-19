using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync();
        public Task<ArticleDto> GetArticleAsync(int id);
        public void DeleteArticle();
        public Task<bool> SaveAllAsync();
    }
}