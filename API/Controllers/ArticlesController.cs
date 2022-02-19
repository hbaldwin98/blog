using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ArticlesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public ArticlesController(DataContext context, IArticleRepository articleRepository, 
                IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _articleRepository = articleRepository;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var article = await _articleRepository.GetArticleAsync(id);

            if (article == null) return NotFound("Article does not exist");
            
            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<ArticleDto>> GetArticles()
        {
            var articles = await _articleRepository.GetArticlesAsync();

            return Ok(articles);
        }
    }
}