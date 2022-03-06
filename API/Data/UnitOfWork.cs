using API.Interfaces;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IUserRepository UserRespository => new UserRepository(_context, _mapper);

        public ICommentRepository CommentRepository => new CommentRepository(_context, _mapper);

        public IArticleRepository ArticleRepository => new ArticleRepository(_context, _mapper);

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> Update()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}