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

public class UpdatePostRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public UpdatePostRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task UpdatePostTest()
    {
        var handler = new UpdatePostRequestHandler(_mockRepo.Object, _mapper);
        var postId = (await _mockRepo.Object.PostRepository.GetAll())[0].Id;
        var userId = (await _mockRepo.Object.PostRepository.GetAll())[0].UserId;
     
        UpdatePostDto updatedPost = new UpdatePostDto{
            Id = postId,
            Content = "Updated Test"
        };
    var result = await handler.Handle(new UpdatePostRequest(updatedPost), CancellationToken.None);
    result.ShouldBeOfType<Application.Responses.BaseCommandResponse>();
    result.Success.ShouldBeTrue();
    result.Message.ShouldBe("Successfully updated the post");
        
    }
}