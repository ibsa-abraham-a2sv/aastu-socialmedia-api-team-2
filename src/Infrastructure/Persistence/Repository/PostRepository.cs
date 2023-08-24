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

    public async Task<List<Post>> GetPosts()
    {

        var results = await _dbContext.Posts.ToListAsync();
        return results;

    }
    public async Task<Guid> CreatePost(PostDto postDto)
{
    var newPost = new Post
    {
        UserId = postDto.UserId,
        Content = postDto.Content,
        CreatedAt = DateTime.UtcNow
   
    };

    _dbContext.Posts.Add(newPost);
    await _dbContext.SaveChangesAsync();
    return newPost.Id;
}

    public async Task UpdatePost(UpdatePostDto updatePost)
    {
        

        var existingPost = await _dbContext.Posts.FindAsync(updatePost.Id);
        if (existingPost != null && existingPost.UserId == updatePost.UserId)
        {
            existingPost.Content = updatePost.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;
            _dbContext.Update(existingPost);
            await _dbContext.SaveChangesAsync(); 

        }
        
    }
    public async Task DeletePost(DeletePostDto postDeleteDto)
{
   
    var existingPost = await _dbContext.Posts.FindAsync(postDeleteDto.PostId);
    Console.WriteLine(existingPost);

    if (existingPost != null && existingPost.UserId == postDeleteDto.UserId)
    {
        _dbContext.Posts.Remove(existingPost);
        await _dbContext.SaveChangesAsync();
    }
}

    public async Task<List<Post>> GetPostsByUserId(Guid userId)
    {
        var results = await _dbContext.Posts.Where(post => post.UserId == userId).ToListAsync();
        return results;
    }
   

}