using Application.Contracts.Persistence;
using Application.Features.Likes.Handlers.Commands;
using Application.Features.Likes.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Likes.Commands;

public class RemoveLikeRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public RemoveLikeRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task RemoveLikeTest()
    {
        var handler = new RemoveLikeRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.LikesRepository.GetAll())[0].UserId;
        var likesId = (await _mockRepo.Object.LikesRepository.GetAll())[0].LikesId;
        
        var result = await handler.Handle(new RemoveLikeRequest(userId, likesId), CancellationToken.None);
     
        result.ShouldBeOfType<Unit>();
        
        (await _mockRepo.Object.LikesRepository.GetAll()).Count.ShouldBe(2);

    }
    
}