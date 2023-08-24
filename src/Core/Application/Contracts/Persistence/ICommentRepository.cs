using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Comment;

namespace Application.Contracts.Persistence
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IReadOnlyList<Comment>> GetCommentsByPostId(Guid PostId, int pageIndex, int pageSize);
        Task<IReadOnlyList<Comment>> GetCommentsByUserId(Guid UserId, int pageIndex, int pageSize);
    }
}