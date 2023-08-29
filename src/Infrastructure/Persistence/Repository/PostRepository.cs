using Application.Contracts.Persistence;
using Application.DTOs.Post;
using Domain.Post;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly SocialSyncDbContext _dbContext;
    public PostRepository(SocialSyncDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<Post> GetPost(Guid postId)
    {
        var results = await _dbContext.Posts.Where(post => post.Id == postId).FirstOrDefaultAsync();
        return results;
    }

    public async Task<List<Post>> GetPosts(int pageIndex = 1, int pageSize = 10)
    {

        var results = await _dbContext.Posts.OrderByDescending(p => p.CreatedAt)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(); ;
        return results;
    }
    public async Task<Guid> CreatePost(Post post)
    {
        // var newPost = new Post
        // {
        //     UserId = postDto.UserId,
        //     Content = postDto.Content,
        //     CreatedAt = DateTime.UtcNow

        // };

        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
        return post.Id;
    }

    public async Task<Guid> UpdatePost(Post post)
    {


        var existingPost = await _dbContext.Posts.FindAsync(post.Id);
        if (existingPost != null && existingPost.UserId == post.UserId)
        {
            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;
            _dbContext.Update(existingPost);
            await _dbContext.SaveChangesAsync();
        }
         return existingPost.Id;
    }
    public async Task DeletePost(Guid userId, Guid postId)
    {

        var existingPost = await _dbContext.Posts.FindAsync(postId);
        Console.WriteLine(existingPost);

        if (existingPost != null && existingPost.UserId == userId)
        {
            _dbContext.Posts.Remove(existingPost);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Post>> GetPostsByUserId(Guid userId, int pageIndex = 1, int pageSize = 10)
    {
        var results = await _dbContext.Posts.Where(post => post.UserId == userId).OrderByDescending(p => p.CreatedAt)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize).ToListAsync();
        return results;
    }
    public async Task<List<Post>> GetFollowingPosts(Guid userId)
    {
        var following = await _dbContext.Follows.Where(follow => follow.UserId == userId).ToListAsync();
        var posts = new List<Post>();

        foreach (var follow in following)
        {
            var userPosts = await GetPostsByUserId(follow.FollowsId);
            posts.AddRange(userPosts);
        }
        return posts;
    }

    public async Task<IReadOnlyList<Post>> GetPostsByHashtag(string tag, int pageIndex, int pageSize)
    {
        return await _dbContext.Posts.Where(post => post.PostHashtags.Any(ph => ph.Hashtag.Tag.Equals(tag))).OrderByDescending(p => p.CreatedAt)
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize).ToListAsync();
    }
}