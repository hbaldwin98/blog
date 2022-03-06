using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        ///<summary>Takes a comment and an article and adds the comment to the article.</summary>
        public void AddComment(Comment comment, Article article)
        {
            article.Comments.Add(comment);
        }
        ///<summary>Delete a comment from the database.</summary>
        public void DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
        }
        ///<summary>Retrieve a specified comment from the database.</summary>
        ///<returns>The desired comment as a CommentDto</returns>
        public async Task<CommentDto> GetComment(int id)
        {
            return await _context.Comments
                .Where(i => i.Id == id)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}