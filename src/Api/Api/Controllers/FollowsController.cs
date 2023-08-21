using Application.Features.Follows.Requests.Commands;
using Application.Features.Follows.Requests.Queries;
using Application.Responses;
using Domain.Follows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]/{id:guid}")]
[ApiController]
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