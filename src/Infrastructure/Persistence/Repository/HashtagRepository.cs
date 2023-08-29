using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain.Hashtag;
using Domain.Post;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public class HashtagRepository : GenericRepository<Hashtag>, IHashtagRepository
    {
        private readonly SocialSyncDbContext _dbContext;
        public HashtagRepository(SocialSyncDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddHashtagToPost(Guid postId, Guid hashtagId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            var hashtag = await _dbContext.Hashtags.FirstOrDefaultAsync(h => h.Id == hashtagId);

            if (post != null && hashtag != null)
            {
                var postHashtag = new PostHashtag
                {
                    PostId = postId,
                    HashtagId = hashtagId,
                    Hashtag = hashtag,
                    Post = post
                };

                post.PostHashtags.Add(postHashtag);
                _dbContext.SaveChanges();
            }
        }

        public async Task<Hashtag?> GetByTag(string tag)
        {
            return await _dbContext.Hashtags.FirstOrDefaultAsync(h => h.Tag == tag);
        }

        public async Task<IReadOnlyList<Post>> GetPostsByHashtag(string tag, int pageIndex, int pageSize)
        {
            return await _dbContext.Posts.Where(post => post.PostHashtags.Any(ph => ph.Hashtag.Tag.Equals(tag))).OrderByDescending(p => p.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();
        }

        public async Task<IReadOnlyList<Hashtag>> GetHashtagsByPostId(Guid postId, int pageIndex, int pageSize)
        {
            return await _dbContext.Hashtags.Where(hashtag => hashtag.PostHashtags.Any(ph => ph.PostId == postId)).OrderByDescending(h => h.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();
        }

        public async Task RemoveHashtagFromPost(Guid postId, Guid hashtagId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            var hashtag = await _dbContext.Hashtags.FirstOrDefaultAsync(h => h.Id == hashtagId);

            if (post != null && hashtag != null)
            {
                var hashtagPost = post.PostHashtags.FirstOrDefault(hp => hp.HashtagId == hashtagId);

                if (hashtagPost != null)
                {
                    post.PostHashtags.Remove(hashtagPost);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}