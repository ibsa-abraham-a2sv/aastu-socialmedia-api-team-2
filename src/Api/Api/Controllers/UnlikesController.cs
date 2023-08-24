using Application.DTOs.Unlikes;
using Application.Features.Unlikes.Requests.Commands;
using Application.Features.Unlikes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class UnlikesController : ControllerBase
{
    private readonly IMediator _mediator;
    public UnlikesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{userId:guid}/{unlikesId:guid}")]
    public async Task<ActionResult<bool>> CheckUserUnlikes(Guid userId, Guid unlikesId)
    {
        var response = await _mediator.Send(new CheckIfUserUnlikesRequest(userId, unlikesId));
        return Ok(response);
    }

    [HttpPost("{userId:guid}")]
    public async Task<ActionResult<Guid>> CreateUnlike(Guid userId,[FromBody] UnlikesDto unlikes )
    {
        var response = await _mediator.Send(new CreateUnlikeRequest(userId, unlikes.UnlikesId));

        if (response == null) return BadRequest("User already likes this post");

        return Ok(response);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<Unit>> RemoveUnlike(Guid userId,[FromBody] UnlikesDto unlikes)
    {
        var response = await _mediator.Send(new RemoveUnlikeRequest(userId, unlikes.UnlikesId));
        return NoContent();
    }
    
    [HttpGet("{unlikesId:guid}/count")]
    public async Task<ActionResult<int>> GetLikesCount(Guid unlikesId)
    {
        return Ok(await _mediator.Send(new GetUnlikesCountRequest(unlikesId)));
    }
}