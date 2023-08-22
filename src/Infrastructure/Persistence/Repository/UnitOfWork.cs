using Application.Contracts.Persistence;
using Domain.Follows;

namespace Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly SocialSyncDbContext _dbContext;
    private FollowsRepository? _followsRepository;

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

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }
}