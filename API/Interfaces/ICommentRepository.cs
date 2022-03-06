using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment, Article article);
        void DeleteComment(Comment comment);
        Task<CommentDto> GetComment(int id);
    }
}