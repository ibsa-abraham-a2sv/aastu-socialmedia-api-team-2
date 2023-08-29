using Application.Contracts.Persistence;
using Application.Features.Post.Handlers.Queries;
using Application.Features.Post.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Post.Queries;

public class GetPostsByUserIdRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetPostsByUserIdRequestHandlerTest()
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
        var handler = new GetPostsByUserIdRequestHandler(_mockRepo.Object);
     
       
         var userId = (await _mockRepo.Object.PostRepository.GetAll())[0].UserId;
       
    var result = await handler.Handle(new GetPostsByUserIdRequest(userId, 1, 10), CancellationToken.None);
    result.ShouldBeOfType<List<Domain.Post.Post>>();
    
        
        
    }
}