using Application.Contracts.Persistence;
using Application.DTOs.Comment;
using Application.DTOs.Comment.Validators;
using Application.Exceptions;
using Application.Features.Comments.Handlers.Commands;
using Application.Features.Comments.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Comments.Commands
{
    public class UpdateCommentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public UpdateCommentCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidComment_UpdatesComment()
        {
            var handler = new UpdateCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            var commentId = (await _mockUnitOfWork.Object.CommentRepository.GetAll())[0].Id;

            var updateDto = new UpdateCommentDto { Id = commentId, Content = "Updated content" };
            var command = new UpdateCommentCommand { UpdateCommentDto = updateDto };

            await handler.Handle(command, CancellationToken.None);

            var comment = await _mockUnitOfWork.Object.CommentRepository.Get(commentId);
            comment.ShouldNotBeNull();
            comment.Content.ShouldBe("Updated content");
        }

        [Fact]
        public async Task Handle_InvalidComment_ThrowsValidationException()
        {
            var handler = new UpdateCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            var commentId = (await _mockUnitOfWork.Object.CommentRepository.GetAll())[0].Id;

            var updateDto = new UpdateCommentDto { Id = commentId, Content = string.Empty };
            var request = new UpdateCommentCommand { UpdateCommentDto = updateDto };

            await Should.ThrowAsync<ValidationException>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_NonExistentComment_ThrowsNotFoundException()
        {
            var handler = new UpdateCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            var commentId = Guid.NewGuid();
            var updateDto = new UpdateCommentDto { Id = commentId, Content = "Updated content" };
            var request = new UpdateCommentCommand { UpdateCommentDto = updateDto };

            await Should.ThrowAsync<NotFoundException>(async () => await handler.Handle(request, CancellationToken.None));
        }
    }
}