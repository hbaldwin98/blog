using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{title}")]
        public async Task<ActionResult<ArticleDto>> GetArticleTitle(string title)
        {
            var article = await _articleRepository.GetArticleByTitleAsync(title);

            if (article == null) return NotFound("Article does not exist.");

            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<ArticleDto>> GetArticles()
        {
            var articles = await _articleRepository.GetArticlesAsync();

            return Ok(articles);
        }


        [HttpDelete("delete-article/{id}")]
        
        public async Task<ActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(i => i.Id == id);

            if (article == null) return NotFound("Article does not exist");

            _articleRepository.DeleteArticle(article);

            if (await _articleRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete article");
        }

    }
}