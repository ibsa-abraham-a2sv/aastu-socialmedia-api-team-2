using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Hashtag;
using Domain.Post;

namespace Application.Contracts.Persistence
{
    public interface IHashtagRepository : IGenericRepository<Hashtag>
    {
        Task<Hashtag?> GetByTag(string tag);
        Task<IReadOnlyList<Hashtag>> GetHashtagsByPostId(Guid postId, int pageIndex, int pageSize);
        Task AddHashtagToPost(Guid postId, Guid hashtagId);
        Task RemoveHashtagFromPost(Guid postId, Guid hashtagId);
        Task<IReadOnlyList<Post>> GetPostsByHashtag(string tag, int pageIndex, int pageSize);
    }
}