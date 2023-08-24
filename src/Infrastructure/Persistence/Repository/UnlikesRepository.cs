using Application.Contracts.Persistence;
using Domain.Likes;
using Domain.Unlikes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;

public class UnlikesRepository : GenericRepository<Unlikes>, IUnlikesRepository
{
    private readonly SocialSyncDbContext _dbContext;
    public UnlikesRepository(SocialSyncDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CheckIfUserUnlikes(Guid userId, Guid unlikesId)
    {
        return await _dbContext.Unlikes.Where(u => 
            u.UserId == userId && u.UnlikesId == unlikesId).FirstOrDefaultAsync() != null;
    }

    public async Task<Guid?> CreateUnlike(Guid userId, Guid unlikesId)
    {
        var isAlreadyUnliked = await CheckIfUserUnlikes(userId, unlikesId);
        
        if (isAlreadyUnliked) return null;
                
        var response = await _dbContext.Unlikes.AddAsync(new Unlikes(){UnlikesId = unlikesId, UserId = userId});
        
        await _dbContext.SaveChangesAsync();
        
        return response.Entity.Id;
    }

    public async Task<Unit> RemoveUnlike(Guid userId, Guid unlikesId)
    {
        var entry = await _dbContext.Unlikes.Where(u => u.UserId == userId && u.UnlikesId == unlikesId).FirstOrDefaultAsync();
                
        if (entry != null)
        {
            _dbContext.Unlikes.Remove(entry);
        }

        await _dbContext.SaveChangesAsync();
                        
        return Unit.Value;
    }

    public async Task<int> GetUnlikeCounts(Guid unlikesId)
    {
        return await _dbContext.Unlikes.Where(u => u.UnlikesId == unlikesId).CountAsync();
    }
}