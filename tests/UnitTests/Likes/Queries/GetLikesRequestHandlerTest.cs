using Application.Contracts.Persistence;
using Application.DTOs.Likes;
using Application.Features.Likes.Handlers.Queries;
using Application.Features.Likes.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Likes.Queries;

public class GetLikesRequestHandlerTest
{
    
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetLikesRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetLikesTest()
    {
        var handler = new GetLikesRequestHandler(_mockRepo.Object, _mapper);

        var userId = (await _mockRepo.Object.LikesRepository.GetAll())[0].UserId;
        
        var result = await handler.Handle(new GetLikesRequest(userId), CancellationToken.None);

        result.ShouldBeOfType<List<LikesDto>>();

        result.Count.ShouldBe(2);
    }  
}