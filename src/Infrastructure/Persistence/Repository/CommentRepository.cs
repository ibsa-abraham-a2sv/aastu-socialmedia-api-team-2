using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Comment;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly SocialSyncDbContext _dbContext;

        public CommentRepository(SocialSyncDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByPostId(Guid PostId, int pageIndex, int pageSize)
        {
            var comments = await _dbContext.Comments.Where(c => c.PostId == PostId)
                                .OrderByDescending(c => c.CreatedAt)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return comments;
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsByUserId(Guid UserId, int pageIndex, int pageSize)
        {
            var comments = await _dbContext.Comments.Where(c => c.UserId == UserId)
                                .OrderByDescending(c => c.CreatedAt)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            return comments;
        }
    }
}