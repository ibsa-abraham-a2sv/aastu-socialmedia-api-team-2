using Application.DTOs.Post;
using Domain.Post;
using MediatR;

namespace Application.Contracts.Persistence;

public interface IPostRepository: IGenericRepository<Post>
{
    Task<Post> GetPost(Guid postId);
    Task<List<Post>> GetPostsByUserId(Guid userId);

    Task<List<Post>> GetPosts();
    Task<Guid> CreatePost(PostDto postDto);
    Task UpdatePost(UpdatePostDto updatePost);
    Task DeletePost(DeletePostDto deletePost);
}
