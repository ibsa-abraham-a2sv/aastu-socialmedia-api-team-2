using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Queries;
using Application.Features.Follows.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Queries;

public class CheckUserFollowsRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CheckUserFollowsRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task CheckUserFollowTest()
    {
        var handler = new CheckIfUserFollowsRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
        var userId2 = (await _mockRepo.Object.FollowsRepository.GetAll())[0].FollowsId;
        

        var result = await handler.Handle(new CheckIfUserFollowsRequest(userId, userId2), CancellationToken.None);
        
        result.ShouldBeOfType<bool>();

        result.ShouldBe(true);
    } 
}