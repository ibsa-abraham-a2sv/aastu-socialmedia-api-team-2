﻿using Application.Contracts.Persistence;
using Application.Features.Follows.Handlers.Queries;
using Application.Features.Follows.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using UnitTests.Mocks;

namespace UnitTests.Follows.Queries;
public class GetFollowingRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _mockRepo;
    public GetFollowingRequestHandlerTest()
    {
        _mockRepo = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

    }

    [Fact]  
    public async Task GetFollowingCountTest()
    {
        var handler = new GetFollowingRequestHandler(_mockRepo.Object);

        var userId = (await _mockRepo.Object.FollowsRepository.GetAll())[0].UserId;
        

        var result = await handler.Handle(new GetFollowingRequest(userId), CancellationToken.None);

        result.ShouldBeOfType<List<Domain.Follows.Follows>>();

        result.Count.ShouldBe(1);
    }  
}
