namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRespository {get;}
        ICommentRepository CommentRepository {get;}
        IArticleRepository ArticleRepository {get;}
        Task<bool> Update();
        bool HasChanges();
    }
}