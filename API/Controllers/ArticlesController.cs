using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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

        [HttpPost("post-article/{id}")]
        public async Task<ActionResult> PostArticle(int id, CreateArticleDto articleDto)
        {

            var user = await _context.Users
                .Where(i => i.Id == id)
                .Include(a => a.Articles)
                .SingleOrDefaultAsync();
                
            if (user == null) return BadRequest("User does not exist");

            if (await _articleRepository.ArticleExists(articleDto.Title.ConvertUrl())) 
                return BadRequest("Article with that title already exists.");

            user.Articles.Add(new Article
            {
                AuthorId = user.Id,
                UrlIdentity = articleDto.Title.ConvertUrl(),
                Title = articleDto.Title,
                Contents = articleDto.Contents,
                Tags = articleDto.Tags
            });

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to create article");
        }

    }
}