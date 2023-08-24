using Application.Contracts.Persistence;
using MediatR;
using Moq;

namespace UnitTests.Mocks;

public static class MockFollowsRepository
{
    public static Mock<IFollowsRepository> GetFollowsRepository()
    {
        var users = new List<Tuple<string, Guid>>
        {
            new Tuple<string, Guid>("Nathnael", Guid.NewGuid()),
            new Tuple<string, Guid>("Yonas", Guid.NewGuid()),
            new Tuple<string, Guid>("Fuad", Guid.NewGuid()),
            new Tuple<string, Guid>("Nardos", Guid.NewGuid()),
            new Tuple<string, Guid>("Kidus", Guid.NewGuid())
        };
        
        var followsList = new List<Domain.Follows.Follows>
        {
            new Domain.Follows.Follows()
            {
                UserId = users[0].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                FollowsId = users[1].Item2
            },
            new Domain.Follows.Follows()
            {
                UserId = users[1].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                FollowsId = users[2].Item2
            },
            new Domain.Follows.Follows()
            {
                UserId = users[2].Item2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                FollowsId = users[0].Item2
            },
        };

        var mockRepo = new Mock<IFollowsRepository>();

        mockRepo.Setup(r => r.Follow(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid followsId) =>
        {
            var follow = new Domain.Follows.Follows()
                { FollowsId = followsId, UserId = userId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            followsList.Add(follow);
            return follow.Id;
        });

        mockRepo.Setup(r => r.CheckIfUserFollows(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(
            (Guid userId, Guid followsId) =>
            {
                return followsList.Find(f => f.FollowsId == followsId && f.UserId == userId) != null;

            });
        
        mockRepo.Setup(r => r.Unfollow(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid followsId) =>
        {
            var response = followsList.FirstOrDefault(u => u.FollowsId == followsId && u.UserId == userId);

            if (response != null)
            {
                followsList.Remove(response);
            }

            return new Unit();
        });
        
        mockRepo.Setup(r => r.GetFollowers(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var response = followsList.Where(u => u.FollowsId == userId).ToList();

            return response;
        });
        
        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(followsList);
        
        mockRepo.Setup(r => r.GetFollowing(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var response = followsList.Where(u => u.UserId == userId).ToList();
                
            return response;
        });
        
        mockRepo.Setup(r => r.GetFollowersCount(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var response = followsList.Count(u => u.FollowsId == userId);
                        
            return response;
        });
        
        mockRepo.Setup(r => r.GetFollowingCount(It.IsAny<Guid>())).ReturnsAsync((Guid userId) =>
        {
            var response = followsList.Count(u => u.UserId == userId);
                                
            return response;
        });
        
        return mockRepo;

    } 
    
}