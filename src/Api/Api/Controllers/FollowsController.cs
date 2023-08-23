using Application.DTOs.Follows;
using Application.Features.Follows.Requests.Commands;
using Application.Features.Follows.Requests.Queries;
using Application.Responses;
using Domain.Follows;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/{id:guid}")]
[ApiController]
//[Authorize]
public class FollowsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FollowsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("followers")]
    public async Task<ActionResult<List<Follows>>> GetFollowers(Guid id)
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

    [HttpPost]
    public async Task<ActionResult<BaseCommandResponse>> CreateFollowing(Guid id, [FromBody] FollowsDto follows)
    {
        var response = await _mediator.Send(new CreateFollowingRequest(id, follows.FollowsId));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> RemoveFollowing(Guid id, [FromBody] FollowsDto follows)
    {
        var response = await _mediator.Send(new RemoveFollowingRequest(id, follows.FollowsId));
        return NoContent();
    }
    
    [HttpGet("following/count")]
    public async Task<ActionResult<List<Follows>>> GetFollowingCount(Guid id)
    {
        var following = await _mediator.Send(new GetFollowingCountRequest(id));
        return Ok(following);
    }
    
    [HttpGet("followers/count")]
    public async Task<ActionResult<List<Follows>>> GetFollowersCount(Guid id)
    {
        var following = await _mediator.Send(new GetFollowersCountRequest(id));
        return Ok(following);
    }
    
    [HttpGet("check/{id:guid}/{followsId:guid}")]
    public async Task<ActionResult<bool>> CheckUserFollows(Guid id, Guid followsId)
    {
        return Ok(await _mediator.Send(new CheckIfUserFollowsRequest(id, followsId)));
    }
}