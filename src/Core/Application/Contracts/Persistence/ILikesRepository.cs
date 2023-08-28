using Domain.Likes;
using MediatR;

namespace Application.Contracts.Persistence;

public interface ILikesRepository : IGenericRepository<Likes>
{
    Task<bool> CheckIfUserLikes(Guid userId, Guid likesId);
    Task<Guid?> CreateLike(Guid userId, Guid likesId);
    Task<Unit> RemoveLike(Guid userId, Guid likesId);

    Task<List<Likes>> GetLikedContentList(Guid userId);

    Task<int> GetLikesCount(Guid likesId);
}