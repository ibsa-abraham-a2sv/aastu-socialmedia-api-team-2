using Application.Contracts.Persistence;
using Moq;

namespace UnitTests.Mocks;

public static class MockUnitOfWork
{
    
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {


        var mockUow = new Mock<IUnitOfWork>();

        mockUow.Setup(r => r.FollowsRepository).Returns(MockFollowsRepository.GetFollowsRepository().Object);
        mockUow.Setup(r => r.LikesRepository).Returns(MockLikesRepository.GetLikesRepository().Object);
        mockUow.Setup(r => r.UnlikesRepository).Returns(MockUnlikesRepository.GetUnlikesRepository().Object);

        return mockUow;
    }
}