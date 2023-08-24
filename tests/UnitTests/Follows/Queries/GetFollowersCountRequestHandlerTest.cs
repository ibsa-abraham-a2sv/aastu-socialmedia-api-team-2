using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Queries;
using Application.Features.Follows.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Queries;

public class GetFollowersCountRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetFollowersCountRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetFollowersCountTest()
    {
        var handler = new GetFollowersCountRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
        

        var result = await handler.Handle(new GetFollowersCountRequest(userId), CancellationToken.None);

        result.ShouldBeOfType<int>();

        result.ShouldBe(1);
    } 
}