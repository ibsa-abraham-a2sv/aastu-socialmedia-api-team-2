using Application.DTOs.Likes;
using Application.Features.Likes.Requests.Commands;
using Application.Features.Likes.Requests.Queries;
using Domain.Likes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class LikesController : ControllerBase
{
    private readonly IMediator _mediator;
    public LikesController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<List<Likes>>> GetLikes(Guid userId)
    {
        var response = await _mediator.Send(new GetLikesRequest(userId));
        return Ok(response);
    }

    [HttpGet("{userId:guid}/{likesId:guid}")]
    public async Task<ActionResult<bool>> CheckUserLikes(Guid userId, Guid likesId)
    {
        var response = await _mediator.Send(new CheckIfUserLikesRequest(userId, likesId));
        return Ok(response);
    }

    [HttpPost("{userId:guid}")]
    public async Task<ActionResult<Guid>> CreateLike(Guid userId, [FromBody] LikesDto likes)
    {
        var response = await _mediator.Send(new CreateLikeRequest(userId, likes.LikesId));

        if (response == null) return BadRequest("User already likes this post");

        return Ok(response);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult<Unit>> RemoveLike(Guid userId, [FromBody] LikesDto likes)
    {
        await _mediator.Send(new RemoveLikeRequest(userId, likes.LikesId));
        return NoContent();
    }

    [HttpGet("{likesId:guid}/count")]
    public async Task<ActionResult<int>> GetLikesCount(Guid likesId)
    {
        return Ok(await _mediator.Send(new GetLikesCountRequest(likesId)));
    }
}