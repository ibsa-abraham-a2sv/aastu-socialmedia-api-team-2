using Application.Contracts.Persistence;
using Domain.Unlikes;
using MediatR;
using Moq;

namespace UnitTests.Mocks;

public static class MockUnlikesRepository
{
    public static Mock<IUnlikesRepository> GetUnlikesRepository()
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
                
            var unlikesList = new List<Unlikes>
            {
                new Unlikes()
                {
                    UserId = users[0].Item2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UnlikesId = posts[1].Item2
                },
                new Unlikes()
                {
                    UserId = users[1].Item2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UnlikesId = posts[2].Item2
                },
                new Unlikes()
                {
                    UserId = users[2].Item2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UnlikesId = posts[0].Item2
                },
            };
        
            var mockRepo = new Mock<IUnlikesRepository>();
        
            mockRepo.Setup(r => r.CreateUnlike(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid unlikesId) =>
            {
                var unlike = new Unlikes()
                    { UnlikesId = unlikesId, UserId = userId, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
                unlikesList.Add(unlike);
                return unlike.Id;
            });
                
            mockRepo.Setup(r => r.RemoveUnlike(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid unlikesId) =>
            {
                var response = unlikesList.FirstOrDefault(u => u.UnlikesId == unlikesId && u.UserId == userId);
        
                if (response != null)
                {
                    unlikesList.Remove(response);
                }
        
                return new Unit();
            });
                
            mockRepo.Setup(r => r.GetUnlikeCounts(It.IsAny<Guid>())).ReturnsAsync((Guid unlikesId) =>
            {
                var response = unlikesList.Count(u => u.UnlikesId == unlikesId);
                                
                return response;
            });
                
            mockRepo.Setup(r => r.CheckIfUserUnlikes(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid userId, Guid unlikesId) =>
            {
                return unlikesList.Find(l => l.UserId == userId && l.UnlikesId == unlikesId) != null;
            });
            
            return mockRepo;
        
        } 
}