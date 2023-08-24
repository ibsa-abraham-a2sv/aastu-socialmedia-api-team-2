using Application.DTOs.Post;
using Application.Features.Post.Requests.Command;
using Application.Features.Post.Requests.Queries;
using Application.Responses;
using Domain.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api")]
[ApiController]
// [Authorize]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator) => _mediator = mediator;

    [HttpGet("posts/{pageIndex}/{pageSize}")]
    public async Task<ActionResult<List<Post>>> GetPosts(int pageIndex, int pageSize)
    {
        if (pageIndex < 1 || pageSize < 1)
    {
        return BadRequest("Invalid page index or page size.");
    }
        var posts = await _mediator.Send(new GetPostsRequest(pageIndex, pageSize));
        return Ok(posts);
    }
[HttpGet("posts/{postId}")]
public async Task<ActionResult<Post>> GetPostById(Guid postId)
{
    var request = new GetPostRequest(postId);
    var post = await _mediator.Send(request);

    if (post == null)
    {
        return NotFound(); 
    }

    return Ok(post);
}
[HttpGet("posts/user/{userId}/{pageIndex}/{pageSize}")]
public async Task<ActionResult<Post>> GetPostsByUserId(Guid userId, int pageIndex, int pageSize)
{
    if (pageIndex < 1 || pageSize < 1)
    {
        return BadRequest("Invalid page index or page size.");
    }
    var request = new GetPostsByUserIdRequest(userId, pageIndex, pageSize);
    var posts = await _mediator.Send(request);
    return Ok(posts);
}
[HttpPost("posts")]
public async Task<ActionResult<BaseCommandResponse>> CreatePost(PostDto postDto)
{
    var request = new CreatePostRequest(postDto);

    var response = await _mediator.Send(request);

    if (response.Success)
    {
        return Ok(response);
    }

    return BadRequest(response);
}
[HttpPatch("posts")]
public async Task<ActionResult<BaseCommandResponse>> UpdatePost(UpdatePostDto  postUpdateDto)
{
    var request = new UpdatePostRequest(postUpdateDto);

    var response = await _mediator.Send(request);

    if (response.Success)
    {
        return Ok(response);
    }

    return BadRequest(response);
}
[HttpDelete("posts")]
public async Task<ActionResult<BaseCommandResponse>> DeletePost(Guid userId, Guid postId)
{
    var request = new DeletePostRequest(userId, postId);

    var response = await _mediator.Send(request);

    if (response.Success)
    {
        return Ok(response);
    }

    return BadRequest(response);
}
[HttpGet("following/posts/{userId}/{pageIndex}/{pageSize}")]
public async Task<ActionResult<Post>> GetFollowingPosts(Guid userId, int pageIndex, int pageSize)
{
    
        if (pageIndex < 1 || pageSize < 1)
    {
        return BadRequest("Invalid page index or page size.");
    }
    var request = new GetFollowingPostsRequest(userId, pageIndex, pageSize);
    var posts = await _mediator.Send(request);
    return Ok(posts);
}

   
}