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

public class DeletePostRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public DeletePostRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task DeletePostTest()
    {
        var handler = new DeletePostRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.PostRepository.GetAll())[0].UserId;
         var postId = (await _mockRepo.Object.PostRepository.GetAll())[0].Id;
        
    var result = await handler.Handle(new DeletePostRequest(userId, postId), CancellationToken.None);
    result.ShouldBeOfType<Application.Responses.BaseCommandResponse>();
    result.Success.ShouldBeTrue();
    result.Message.ShouldBe("Successfully deleted the post");
  
        
        
    }
}