using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Commands;
using Application.Features.Follows.Requests.Commands;
using Application.Profiles;
using Application.Responses;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Commands;

public class CreateFollowingRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CreateFollowingRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task CreateFollowingTest()
    {
        var handler = new CreateFollowingRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
             
     
        var result = await handler.Handle(new CreateFollowingRequest(userId, Guid.NewGuid()), CancellationToken.None);
     
        result.ShouldBeOfType<BaseCommandResponse>();
        
        result.Success.ShouldBeTrue();
    }

}