using Application.DTOs.Post;
using Domain.Post;
using MediatR;

namespace Application.Contracts.Persistence;

public interface IPostRepository: IGenericRepository<Post>
{
    Task<Post> GetPost(Guid postId);
    Task<List<Post>> GetPostsByUserId(Guid userId, int pageIndex, int pageSize);

    Task<List<Post>> GetPosts(int pageIndex, int pageSize);
    Task<List<Post>> GetFollowingPosts(Guid userId); 
    Task<Guid> CreatePost(Post post);
    Task<Guid> UpdatePost(Post post);
    Task DeletePost(Guid userId, Guid postId);

}
