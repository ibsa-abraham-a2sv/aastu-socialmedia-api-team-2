using Application.Contracts.Persistence;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialSyncDbContext _dbContext;

    public UnitOfWork(SocialSyncDbContext dbContext, IFollowsRepository followsRepository)
    {
        _dbContext = dbContext;
        FollowsRepository = followsRepository;
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public IFollowsRepository FollowsRepository { get; }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}