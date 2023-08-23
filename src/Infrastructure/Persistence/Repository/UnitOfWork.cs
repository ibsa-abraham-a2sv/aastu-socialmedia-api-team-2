using Application.Contracts.Persistence;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialSyncDbContext _dbContext;
    private IFollowsRepository? _followsRepository;
    private ILikesRepository? _likesRepository;
    private IUnlikesRepository? _unlikesRepository;

    public UnitOfWork(SocialSyncDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

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