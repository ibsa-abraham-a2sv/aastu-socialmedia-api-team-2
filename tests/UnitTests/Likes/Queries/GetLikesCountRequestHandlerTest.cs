using Application.Contracts.Persistence;
using Application.Features.Likes.Handlers.Queries;
using Application.Features.Likes.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Likes.Queries;

public class GetLikesCountRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetLikesCountRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetLikesCountTest()
    {
        var handler = new GetLikesCountRequestHandler(_mockRepo.Object);

        var likesId = (await _mockRepo.Object.LikesRepository.GetAll())[0].LikesId;
        
        var result = await handler.Handle(new GetLikesCountRequest(likesId), CancellationToken.None);

        result.ShouldBeOfType<int>();

        result.ShouldBe(1);
    }  
}