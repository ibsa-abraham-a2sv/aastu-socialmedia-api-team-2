using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Commands;
using Application.Features.Follows.Requests.Commands;
using Application.Profiles;
using Application.Responses;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Commands;

public class RemoveFollowingRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public RemoveFollowingRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task RemoveFollowingTest()
    {
        var handler = new RemoveFollowingRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
        var userId2 = (await _mockRepo.Object.FollowsRepository.GetAll())[1].UserId;
        
        var result = await handler.Handle(new RemoveFollowingRequest(userId, userId2), CancellationToken.None);
     
        result.ShouldBeOfType<Unit>();
        
        (await _mockRepo.Object.FollowsRepository.GetAll()).Count.ShouldBe(2);

    }
    
}