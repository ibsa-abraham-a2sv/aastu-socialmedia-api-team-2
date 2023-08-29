using Application.Contracts.Persistence;
using Application.Features.Post.Handlers.Queries;
using Application.Features.Post.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Post.Queries;

public class GetFollowingPostsRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetFollowingPostsRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task GetFollowingPostTest()
    {
        var handler = new GetFollowingPostsRequestHandler(_mockRepo.Object, _mapper);
     
        var userId = (await _mockRepo.Object.PostRepository.GetAll())[0].UserId;
       
    var result = await handler.Handle(new GetFollowingPostsRequest(userId), CancellationToken.None);
    result.ShouldBeOfType<List<Domain.Post.Post>>();
    

    }
}