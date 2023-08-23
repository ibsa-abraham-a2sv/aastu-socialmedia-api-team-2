using Application.Contracts.Persistence;
using Moq;

namespace UnitTests.Mocks;

public static class MockUnitOfWork
{
    
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockFollowsRepository = MockFollowsRepository.GetFollowsRepository();

        mockUow.Setup(r => r.FollowsRepository).Returns(mockFollowsRepository.Object);

        return mockUow;
    }
}