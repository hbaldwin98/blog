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

        public void DeleteArticle()
        {
            throw new NotImplementedException();
        }

        public async Task<ArticleDto> GetArticle(int id)
        {
            return await _context.Articles
                .Where(i => i.Id == id)
                .ProjectTo<ArticleDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}