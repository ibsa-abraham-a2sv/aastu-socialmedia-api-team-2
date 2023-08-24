using Application.Contracts.Persistence;
using Application.Features.Unlikes.Handlers.Commands;
using Application.Features.Unlikes.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Unlikes.Commands;

public class RemoveUnlikeRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;

    public RemoveUnlikeRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]
    public async Task RemoveUnlikeTest()
    {
        var handler = new RemoveUnlikesRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UserId;
        var unlikesId = (await _mockRepo.Object.UnlikesRepository.GetAll())[0].UnlikesId;

        var result = await handler.Handle(new RemoveUnlikeRequest(userId, unlikesId), CancellationToken.None);

        result.ShouldBeOfType<Unit>();

        (await _mockRepo.Object.UnlikesRepository.GetAll()).Count.ShouldBe(2);

    }
}