using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.Features.Comments.Handlers.Queries;
using Application.Features.Comments.Requests.Commands;
using Application.Profiles;
using Application.Responses;
using AutoMapper;
using Domain.Comment;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Comments.Handlers.Queries
{
    public class CreateCommentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICommentRepository> _mockCommentRepository;

        public CreateCommentCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();
            _mockCommentRepository = new Mock<ICommentRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var commentId = Guid.NewGuid();
            var createCommentDto = new CreateCommentDto
            {
                PostId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Content = "Test Comment"
            };

            var command = new CreateCommentCommand
            {
                CreateCommentDto = createCommentDto
            };

            var handler = new CreateCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            _mockUnitOfWork.Setup(uow => uow.CommentRepository).Returns(_mockCommentRepository.Object);

            _mockCommentRepository
                .Setup(r => r.Add(It.IsAny<Comment>()))
                .ReturnsAsync((Comment comment) =>
                {
                    comment.Id = commentId;
                    return comment;
                });

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
            result.Message.ShouldBe("Creation Successful");
            result.Id.ShouldBe(commentId);

            _mockCommentRepository.Verify(r => r.Add(It.IsAny<Comment>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsErrorResponse()
        {
            var createCommentDto = new CreateCommentDto
            {
                PostId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Content = ""
            };

            var command = new CreateCommentCommand
            {
                CreateCommentDto = createCommentDto
            };

            var handler = new CreateCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Success.ShouldBeFalse();
            result.Message.ShouldBe("Creation Failed");
            result.Errors.ShouldNotBeEmpty();

            _mockCommentRepository.Verify(r => r.Add(It.IsAny<Comment>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}