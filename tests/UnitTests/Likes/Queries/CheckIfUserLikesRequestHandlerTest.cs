using Application.Contracts.Persistence;
using Application.Features.Likes.Handlers.Queries;
using Application.Features.Likes.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Likes.Queries;

public class CheckIfUserLikesRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CheckIfUserLikesRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task CheckIfUserLikesTest()
    {
        var handler = new CheckIfUserLikesRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.LikesRepository.GetAll())[0].UserId;
        var likesId = (await _mockRepo.Object.LikesRepository.GetAll())[0].LikesId;
        
        var result = await handler.Handle(new CheckIfUserLikesRequest(userId, likesId), CancellationToken.None);

        result.ShouldBeOfType<bool>();

        result.ShouldBe(true);
    }
    
}