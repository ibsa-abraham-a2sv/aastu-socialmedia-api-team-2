using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.Features.Comments.Handlers.Queries;
using Application.Features.Comments.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Comments.Queries
{
    public class GetCommentsByUserIdRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public GetCommentsByUserIdRequestHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetCommentsByUserIdTest()
        {
            var handler = new GetCommentsByUserIdRequestHandler(_mockUnitOfWork.Object, _mapper);

            var userId = (await _mockUnitOfWork.Object.CommentRepository.GetAll())[0].UserId;

            var pageIndex = 1;
            var pageSize = 10;
            var request = new GetCommentsByUserIdRequest{UserId = userId, PageIndex = pageIndex, PageSize = pageSize};

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<CommentsOfUserDto>>();
            result.Count.ShouldBe(1);
        }
    }
}