using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Application.Features.Post.Handlers.Command;
using Application.Features.Post.Requests.Command;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Post.Commands;

public class CreatePostRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CreatePostRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task CreatePostTest()
    {
        var handler = new CreatePostRequestHandler(_mockRepo.Object, _mapper);
     
        var userId = (await _mockRepo.Object.PostRepository.GetAll())[0].UserId;
        PostDto post = new PostDto{
            Id = Guid.NewGuid(),
            UserId = userId,
            Content = "Test"
        };
    var result = await handler.Handle(new CreatePostRequest(post), CancellationToken.None);
    result.ShouldBeOfType<Application.Responses.BaseCommandResponse>();
    result.Success.ShouldBeTrue();
    result.Message.ShouldBe("Successfully created");
    result.Id.ShouldNotBeNull();
        
        
    }
}