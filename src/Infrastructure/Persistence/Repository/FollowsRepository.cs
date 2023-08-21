using Application.Contracts.Persistence;
using Domain.Follows;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;

public class FollowsRepository : GenericRepository<Follows>, IFollowsRepository
{
    private readonly SocialSyncDbContext _dbContext;
    public FollowsRepository(SocialSyncDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckIfUserFollows(Guid userId, Guid followsId)
    {
        return await _dbContext.Follows.Where(user => 
                user.UserId == userId && user.FollowsId == followsId).FirstOrDefaultAsync() != null;
    }

    public async Task<Guid?> Follow(Guid userId, Guid followsId)
    {
        var isExists = await CheckIfUserFollows(userId, followsId);

        if (isExists) return null;
        
        var result = await _dbContext.Follows.AddAsync(new Follows(){FollowsId = followsId, UserId = userId});

        await _dbContext.SaveChangesAsync();

        return result.Entity.Id;
    }

    public async Task<Unit> Unfollow(Guid userId, Guid followsId)
    {
        var entry = await _dbContext.Follows.Where(user => user.UserId == userId && user.FollowsId == followsId).FirstOrDefaultAsync();

        if (entry != null)
        {
            _dbContext.Follows.Remove(entry);
        }
        
        return Unit.Value;
    }

    public async Task<List<Follows>> GetFollowers(Guid userId)
    {
        var results = await _dbContext.Follows.Where(user => user.FollowsId == userId).ToListAsync();

        return results;
    }

    public async Task<List<Follows>> GetFollowing(Guid userId)
    {
        var results = await _dbContext.Follows.Where(user => user.UserId == userId).ToListAsync();

        return results;
    }
}