using Application.Features.Post.Requests.Commands;
using Application.Features.Post.Requests.Queries;
using Application.Responses;
using Domain.Post;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/{id:guid}")]
[ApiController]
[Authorize]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostController(IMediator mediator) => _mediator = mediator;

    [HttpGet("posts")]
    public async Task<ActionResult<List<Post>>> GetPosts(Guid id)
    {
        var followers = await _mediator.Send(new GetFollowersRequest(id));
        return Ok(followers);
    }

    [HttpGet("following")]
    public async Task<ActionResult<List<Follows>>> GetFollowing(Guid id)
    {
        var following = await _mediator.Send(new GetFollowingRequest(id));
        return Ok(following);
    }

    [HttpPost("{followsId:guid}")]
    public async Task<ActionResult<BaseCommandResponse>> CreateFollowing(Guid id, Guid followsId)
    {
        var response = await _mediator.Send(new CreateFollowingRequest(id, followsId));
        return Ok(response);
    }

    [HttpDelete("{followsId:guid}")]
    public async Task<ActionResult<Unit>> RemoveFollowing(Guid id, Guid followsId)
    {
        var response = await _mediator.Send(new RemoveFollowingRequest(id, followsId));
        return NoContent();
    }
}