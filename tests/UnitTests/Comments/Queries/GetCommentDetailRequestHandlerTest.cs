using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.Features.Comments.Handlers.Queries;
using Application.Features.Comments.Requests.Queries;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Comments.Queries
{
    public class GetCommentDetailRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public GetCommentDetailRequestHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetCommentDetailTest()
        {
            var handler = new GetCommentDetailRequestHandler(_mockUnitOfWork.Object, _mapper);

            var commentId = (await _mockUnitOfWork.Object.CommentRepository.GetAll())[0].Id;

            var request = new GetCommentDetailRequest { Id = commentId };

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<CommentDto>();
        }
    }
}