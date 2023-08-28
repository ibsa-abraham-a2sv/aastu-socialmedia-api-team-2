using Application.Contracts.Persistence;
using Application.Features.Unlikes.Handlers.Queries;
using Application.Features.Unlikes.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Unlikes.Queries;

public class GetUnlikesCountRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetUnlikesCountRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetUnlikesCountTest()
    {
        var handler = new GetUnlikesCountRequestHandler(_mockRepo.Object);

        var unlikesId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UnlikesId;
        
        var result = await handler.Handle(new GetUnlikesCountRequest(unlikesId), CancellationToken.None);

        result.ShouldBeOfType<int>();

        result.ShouldBe(2);
    }  
}