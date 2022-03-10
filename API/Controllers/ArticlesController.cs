using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ArticlesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ArticlesController(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<ArticleDto>> GetArticleTitle(string title)
        {
            var article = await _unitOfWork.ArticleRepository.GetArticleByTitleAsync(title);

            if (article == null) return NotFound("Article does not exist.");

            return Ok(article);
        }

        [HttpGet]
        public async Task<ActionResult<ArticleDto>> GetArticles()
        {
            var articles = await _unitOfWork.ArticleRepository.GetArticlesAsync();

            return Ok(articles);
        }

        [Authorize]
        [HttpDelete("delete-article/{url}")]
        public async Task<ActionResult> DeleteArticle(string url)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(i => i.UrlIdentity == url);

            if (article == null) return NotFound("Article does not exist");

            _unitOfWork.ArticleRepository.DeleteArticle(article);

            if (await _unitOfWork.Update()) return Ok();

            return BadRequest("Failed to delete article");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostArticle(CreateArticleDto articleDto)
        {

            var user = await _unitOfWork.UserRespository.GetUserByNameAsync(User.GetUsername());
                
            if (user == null) return BadRequest("User does not exist");

            if (await _unitOfWork.ArticleRepository.ArticleExists(articleDto.Title.ConvertUrl())) 
                return BadRequest("Article with that title already exists.");

            var article = new Article
            {
                AuthorId = user.Id,
                UrlIdentity = articleDto.Title.ConvertUrl(),
                Title = articleDto.Title,
                Contents = articleDto.Contents,
                Headline = articleDto.Headline,
                Tags = articleDto.Tags
            };

            user.Articles.Add(article);

            if (await _unitOfWork.Update()) return Ok(_mapper.Map<ArticleDto>(article));

            return BadRequest("Failed to create article");
        }

    }
}