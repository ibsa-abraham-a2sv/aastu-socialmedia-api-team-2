using Application.Contracts.Persistence;
using Application.Features.Post.Handlers.Queries;
using Application.Features.Post.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Post.Queries;

public class GetPostRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetPostRequestHandlerTest()
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
        var handler = new GetPostRequestHandler(_mockRepo.Object);
     
        var postId = (await _mockRepo.Object.PostRepository.GetAll())[0].Id;
       
    var result = await handler.Handle(new GetPostRequest(postId), CancellationToken.None);
    result.ShouldBeOfType<Domain.Post.Post>();
    
        
        
    }
}