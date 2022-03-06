using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CommentsController(DataContext context, IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetComment(int id)
        {
            var comment = await _unitOfWork.CommentRepository.GetComment(id);

            if (comment == null) return NotFound("Comment does not exist");

            return Ok(comment);
        }

        [HttpPost("post-comment/{urlIdentity}")]
        public async Task<ActionResult> PostComment(string urlIdentity, CreateCommentDto commentDto)
        {
            var article = await _context.Articles
                .Where(i => i.UrlIdentity == urlIdentity)
                .Include(c => c.Comments)
                .SingleOrDefaultAsync();

            if (article == null) return BadRequest("Article does not exist");
            
            var comment = new Comment
            {
                CommenterName = commentDto.CommenterName,
                Contents = commentDto.Contents
            };
            
            _unitOfWork.CommentRepository.AddComment(comment, article);

            if (await _unitOfWork.Update()) return Ok(comment);

            return BadRequest("Error uploading comment");
        }

        [Authorize]
        [HttpDelete("delete-comment/{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
                var comment = await _context.Comments.FirstOrDefaultAsync(i => i.Id == id);
                if (comment == null) return NotFound();

                _unitOfWork.CommentRepository.DeleteComment(comment);

                if (await _unitOfWork.Update()) return Ok();

                return BadRequest("Failed to delete comment");
        }
    }
}