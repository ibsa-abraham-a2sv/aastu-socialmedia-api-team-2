using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Queries;
using Application.Features.Follows.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Queries;

public class GetFollowingCountRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetFollowingCountRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetFollowingCountTestTest()
    {
        var handler = new GetFollowingCountRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
        

        var result = await handler.Handle(new GetFollowingCountRequest(userId), CancellationToken.None);

        result.ShouldBeOfType<int>();

        result.ShouldBe(1);
    }  
}