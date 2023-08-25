using Application.Contracts.Persistence;
using Application.Features.Likes.Handlers.Commands;
using Application.Features.Likes.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Likes.Commands;

public class CreateLikeRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CreateLikeRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task CreateLikeTest()
    {
        var handler = new CreateLikeRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.LikesRepository.GetAll())[0].UserId;
             
     
        var result = await handler.Handle(new CreateLikeRequest(userId, Guid.NewGuid()), CancellationToken.None);
     
        result.ShouldBeOfType<Guid>();
        
        
    }
}