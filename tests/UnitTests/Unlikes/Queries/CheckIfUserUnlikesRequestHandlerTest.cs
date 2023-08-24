using Application.Contracts.Persistence;
using Application.Features.Unlikes.Handlers.Queries;
using Application.Features.Unlikes.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Unlikes.Queries;

public class CheckIfUserUnlikesRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CheckIfUserUnlikesRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task CheckIfUserUnlikesTest()
    {
        var handler = new CheckIfUserUnlikesRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UserId;
        var unlikesId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UnlikesId;
        
        var result = await handler.Handle(new CheckIfUserUnlikesRequest(userId, unlikesId), CancellationToken.None);

        result.ShouldBeOfType<bool>();

        result.ShouldBe(true);
    }
    
}