using Application.Contracts.Persistence;
using Application.Features.Likes.Handlers.Commands;
using Application.Features.Unlikes.Handlers.Commands;
using Application.Features.Unlikes.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Unlikes.Commands;

public class CreateUnlikeRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public CreateUnlikeRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task CreateUnlikeTest()
    {
        var handler = new CreateUnlikeRequestHandler(_mockRepo.Object);
     
        var userId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UserId;
     
        var result = await handler.Handle(new CreateUnlikeRequest(userId, Guid.NewGuid()), CancellationToken.None);
     
        result.ShouldBeOfType<Guid>();
        (await _mockRepo.Object.UnlikesRepository.GetAll()).Count.ShouldBe(4);
    }}