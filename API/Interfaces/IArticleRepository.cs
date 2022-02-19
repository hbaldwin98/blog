using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync();
        public Task<ArticleDto> GetArticleByIdAsync(int id);
        public Task<ArticleDto> GetArticleByTitleAsync(string title);
        public void DeleteArticle(Article article);
        public Task<bool> ArticleExists(string articleUrl);
        public Task<bool> SaveAllAsync();
    }
}