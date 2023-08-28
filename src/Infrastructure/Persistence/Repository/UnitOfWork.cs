using Application.Contracts.Persistence;
using Domain.Follows;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialSyncDbContext _dbContext;

    private CommentRepository? _commentRepository;
    private FollowsRepository? _followsRepository;
    private LikesRepository? _likesRepository;
    private UnlikesRepository? _unlikesRepository;

private PostRepository? _postRepository;


    public UnitOfWork(SocialSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }


       public IPostRepository PostRepository =>
        _postRepository ??= new PostRepository(_dbContext);

    public ICommentRepository CommentRepository => 
            _commentRepository ??= new CommentRepository(_dbContext);
    
    public IFollowsRepository FollowsRepository =>
        _followsRepository ??= new FollowsRepository(_dbContext);

    public ILikesRepository LikesRepository =>
        _likesRepository ??= new LikesRepository(_dbContext);


    public IUnlikesRepository UnlikesRepository =>
        _unlikesRepository ??= new UnlikesRepository(_dbContext);


    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}