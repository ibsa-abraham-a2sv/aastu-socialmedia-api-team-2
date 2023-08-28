using Application.Contracts.Persistence;
using Domain.Comment;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    public static class MockCommentRepository
    {
        public static Mock<ICommentRepository> GetCommentRepository()
        {
            var comments = new List<Comment>
            {
                new Comment
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "Comment 1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "Comment 2",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Comment
                {
                    Id = Guid.NewGuid(),
                    PostId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "Comment 3",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            var mockRepo = new Mock<ICommentRepository>();

            mockRepo.Setup(r => r.GetCommentsByPostId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((Guid postId, int pageIndex, int pageSize) =>
                {
                    var result = comments
                        .Where(c => c.PostId == postId)
                        .OrderByDescending(c => c.CreatedAt)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                    return result;
                });

            mockRepo.Setup(r => r.GetCommentsByUserId(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((Guid userId, int pageIndex, int pageSize) =>
                {
                    var result = comments
                        .Where(c => c.UserId == userId)
                        .OrderByDescending(c => c.CreatedAt)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                    return result;
                });

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(comments);

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync((Guid commentId) =>
                {
                    return comments.FirstOrDefault(c => c.Id == commentId);
                });


            mockRepo.Setup(r => r.Update(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    var existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
                    if (existingComment != null)
                    {
                        existingComment.Content = comment.Content;
                        existingComment.UpdatedAt = DateTime.UtcNow;
                    }
                })
                .Returns(Task.CompletedTask);

            mockRepo.Setup(r => r.Delete(It.IsAny<Comment>()))
                .Callback((Comment comment) =>
                {
                    comments.Remove(comment);
                })
                .Returns(Task.CompletedTask);

            return mockRepo;
        }
    }
}