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
        var results = await _dbContext.Post.Where(post => post.Id == postId).ToListAsync();
        return results;
    }

    public async Task<List<Post>> GetPosts()
    {

        var results = await _dbContext.Post.ToListAsync();
        return results;

    }

    public async Task UpdatePost(UpdatePostDto updatePost)
    {

        var existingPost = await _dbContext.Post.FindAsync(updatePost.PostId);
        if (existingPost != null)
        {

            existingPost.Content = updatePost.Content;


            _dbContext.Update(existingPost);
            await _dbContext.SaveChangesAsync();

        }
    }
    public async Task DeletePost(DeletePostDto postDeleteDto)
{
    var existingPost = await _dbContext.Post.FindAsync(postDeleteDto.PostId);

    if (existingPost != null && existingPost.UserId == postDeleteDto.UserId)
    {
        _dbContext.Post.Remove(existingPost);
        await _dbContext.SaveChangesAsync();
    }
}

    public async Task<List<Post>> GetPostsByUserId(Guid userId)
    {
        var results = await _dbContext.Post.Where(post => post.UserId == userId).ToListAsync();
        return results;
    }
   

}