using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ArticleRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        ///<summary>Deletes a specified article from the database.</summary>
        public void DeleteArticle()
        {
            throw new NotImplementedException();
        }
        ///<returns>Searches and returns a specified article from the database</returns>
        public async Task<ArticleDto> GetArticleAsync(int id)
        {
            return await _context.Articles
                .Where(i => i.Id == id)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            return await _context.Articles
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        ///<summary>Saves changed to the database</summary>
        ///<returns>A boolean indicating a successful or failed database save.</returns>
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}