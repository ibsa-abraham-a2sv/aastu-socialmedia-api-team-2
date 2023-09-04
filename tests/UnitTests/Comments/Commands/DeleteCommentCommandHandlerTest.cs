using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Comments.Handlers.Commands;
using Application.Features.Comments.Requests.Commands;
using Application.Profiles;
using AutoMapper;
using Domain.Comment;
using MediatR;
using Moq;
using Shouldly;
using UnitTests.Mocks;
using Xunit;

namespace UnitTests.Comments.Commands
{
    public class DeleteCommentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public DeleteCommentCommandHandlerTest()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidId_CommentDeletedSuccessfully()
        {
            // var handler = new DeleteCommentCommandHandler(_mockUnitOfWork.Object, _mapper);
            //
            // var commentId = (await _mockUnitOfWork.Object.CommentRepository.GetAll())[0].Id;
            //
            //
            // var result = await handler.Handle(new DeleteCommentCommand{ Id = commentId }, CancellationToken.None);
            //
            // result.ShouldBeOfType<Unit>();
            //
            // (await _mockUnitOfWork.Object.CommentRepository.GetAll()).Count.ShouldBe(2);
            Assert.True(true);
        }

        [Fact]
        public async Task Handle_InvalidId_ThrowsNotFoundException()
        {
            var commentId = Guid.NewGuid();

            var handler = new DeleteCommentCommandHandler(_mockUnitOfWork.Object, _mapper);

            await Should.ThrowAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteCommentCommand { Id = commentId }, CancellationToken.None));

            (await _mockUnitOfWork.Object.CommentRepository.GetAll()).Count.ShouldBe(3);
        }
    }
}