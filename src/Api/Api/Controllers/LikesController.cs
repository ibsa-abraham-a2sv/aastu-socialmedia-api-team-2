using System.Security.Claims;
using Application.Constants;
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
[Authorize]
public class LikesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    public LikesController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Likes>>> GetLikes()
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var response = await _mediator.Send(new GetLikesRequest(new Guid(id)));
        return Ok(response);
    }

    [HttpGet("{likesId:guid}")]
    public async Task<ActionResult<bool>> CheckUserLikes(Guid likesId)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var response = await _mediator.Send(new CheckIfUserLikesRequest(new Guid(id), likesId));
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateLike([FromBody] LikesDto likes)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        var response = await _mediator.Send(new CreateLikeRequest(new Guid(id), likes.LikesId));

        if (response == null) return BadRequest("User already likes this post");

        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> RemoveLike([FromBody] LikesDto likes)
    {
        var id = _contextAccessor.HttpContext!.User.FindFirstValue(CustomClaimTypes.Uid);
        if (id == null) throw new UnauthorizedAccessException("user authentication needed");
        
        await _mediator.Send(new RemoveLikeRequest(new Guid(id), likes.LikesId));
        return NoContent();
    }

    [HttpGet("{likesId:guid}/count")]
    public async Task<ActionResult<int>> GetLikesCount(Guid likesId)
    {
        return Ok(await _mediator.Send(new GetLikesCountRequest(likesId)));
    }
}