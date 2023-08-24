using Application.Contracts.Persistence;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialSyncDbContext _dbContext;
    private FollowsRepository _followsRepository;
    private CommentRepository _commentRepository;

    public UnitOfWork(SocialSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public ICommentRepository CommentRepository => 
            _commentRepository ??= new CommentRepository(_dbContext);
    
    public IFollowsRepository FollowsRepository =>
            _followsRepository ??= new FollowsRepository(_dbContext);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}