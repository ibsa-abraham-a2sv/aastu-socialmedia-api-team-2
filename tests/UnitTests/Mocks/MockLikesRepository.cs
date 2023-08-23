using Application.Contracts.Persistence;
using Domain.Follows;
using Domain.Likes;
using MediatR;
using Moq;

namespace UnitTests.Mocks;

public static class MockLikesRepository
{
    public static Mock<ILikesRepository> GetLikesRepository()
    {
        var users = new List<Tuple<string, Guid>>
        {
            new Tuple<string, Guid>("Nathnael", Guid.NewGuid()),
            new Tuple<string, Guid>("Yonas", Guid.NewGuid()),
            new Tuple<string, Guid>("Fuad", Guid.NewGuid()),
            new Tuple<string, Guid>("Nardos", Guid.NewGuid()),
            new Tuple<string, Guid>("Kidus", Guid.NewGuid())
        };
        
        var posts = new List<Tuple<string, Guid>>
        {
            new Tuple<string, Guid>("Post1", Guid.NewGuid()),
            new Tuple<string, Guid>("Post2", Guid.NewGuid()),
            new Tuple<string, Guid>("Post3", Guid.NewGuid()),
            new Tuple<string, Guid>("Post4", Guid.NewGuid()),
            new Tuple<string, Guid>("Post5", Guid.NewGuid())
        };
            
        var likesList = new List<Likes>
        {
            new Likes()
            {
                UserId = users[0].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LikesId = posts[1].Item2
            },
            new Likes()
            {
                UserId = users[1].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LikesId = posts[2].Item2
            },
            new Likes()
            {
                UserId = users[2].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LikesId = posts[0].Item2
            },
        };
    
        var mockRepo = new Mock<ILikesRepository>();
    
        mockRepo.Setup(r => r.CreateLike(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid likesId) =>
        {
            var like = new Likes()
                { LikesId = likesId, UserId = userId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            likesList.Add(like);
            return like.Id;
        });
            
        mockRepo.Setup(r => r.RemoveLike(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid likesId) =>
        {
            var response = likesList.FirstOrDefault(u => u.LikesId == likesId && u.UserId == userId);
    
            if (response != null)
            {
                likesList.Remove(response);
            }
    
            return new Unit();
        });
            
        mockRepo.Setup(r => r.GetLikesCount(It.IsAny<Guid>())).ReturnsAsync((Guid likesId) =>
        {
            var response = likesList.Count(u => u.LikesId == likesId);
                            
            return response;
        });
            
        mockRepo.Setup(r => r.GetLikedContentList(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var response = likesList.Where(l => l.UserId == userId).ToList();
                                    
            return response;
        });
        
        mockRepo.Setup(r => r.CheckIfUserLikes(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid likesId) =>
        {
            return likesList.Find(l => l.UserId == userId && l.LikesId == likesId) != null;
        });
        
        return mockRepo;
    
    } 
    
}