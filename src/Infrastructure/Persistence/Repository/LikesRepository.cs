using Application.Contracts.Persistence;
using Domain.Likes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;

public class LikesRepository : GenericRepository<Likes>, ILikesRepository
{
    private readonly SocialSyncDbContext _dbContext;
    public LikesRepository(SocialSyncDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<bool> CheckIfUserLikes(Guid userId, Guid likesId)
    {
        return await _dbContext.Likes.Where(l => 
            l.UserId == userId && l.LikesId == likesId).FirstOrDefaultAsync() != null;
    }

    public async Task<Guid?> CreateLike(Guid userId, Guid likesId)
    {
        var isAlreadyLiked = await CheckIfUserLikes(userId, likesId);

        if (isAlreadyLiked) return null;
        
        var response = await _dbContext.Likes.AddAsync(new Likes(){LikesId = likesId, UserId = userId});

        await _dbContext.SaveChangesAsync();

        return response.Entity.Id;
    }

    public async Task<Unit> RemoveLike(Guid userId, Guid likesId)
    {
        var entry = await _dbContext.Likes.Where(l => l.UserId == userId && l.LikesId == likesId).FirstOrDefaultAsync();
        
        if (entry != null)
        {
            _dbContext.Likes.Remove(entry);
        }

        await _dbContext.SaveChangesAsync();
                
        return Unit.Value;
    }

    public async Task<List<Likes>> GetLikedContentList(Guid userId)
    {
        return await _dbContext.Likes.Where(l => l.UserId == userId).ToListAsync();

    }

    public async Task<int> GetLikesCount(Guid likesId)
    {
        return await _dbContext.Likes.Where(l => l.LikesId == likesId).CountAsync();
    }
}