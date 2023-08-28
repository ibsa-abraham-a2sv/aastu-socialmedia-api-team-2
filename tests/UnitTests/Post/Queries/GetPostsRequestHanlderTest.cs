using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Handlers.Command;
using Application.Features.Post.Requests.Command;
using Application.Features.Post.Handlers.Queries;
using Application.Features.Post.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Post.Commands;

public class GetPostsRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetPostsRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task GetPostTest()
    {
        var handler = new GetPostsRequestHandler(_mockRepo.Object);
        var postId = (await _mockRepo.Object.PostRepository.GetAll())[0].Id;
        var result = await handler.Handle(new GetPostsRequest(1, 10), CancellationToken.None);
        result.ShouldBeOfType<List<Domain.Post.Post>>();        
        
    }
}